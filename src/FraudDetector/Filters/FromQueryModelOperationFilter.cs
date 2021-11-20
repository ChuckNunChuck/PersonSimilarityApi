using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FraudDetector.Filters;

public class FromQueryModelOperationFilter : IOperationFilter
{
    private static readonly Type[] TypesToExclude = { typeof(CancellationToken) };

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var description = context.ApiDescription;
        if (!string.Equals(
                description.HttpMethod,
                $"{HttpMethod.Get}",
                StringComparison.OrdinalIgnoreCase)
           )
            return;

        var actionParameters =
            description.ActionDescriptor.Parameters.Where(p => !TypesToExclude.Contains(p.ParameterType))
                .ToList();
        var apiParameters = description.ParameterDescriptions
            .Where(p => p.Source.IsFromRequest)
            .ToList();

        if (actionParameters.Count == apiParameters.Count)
            return;

        operation.Parameters = CreateParameters(actionParameters, operation.Parameters, context);
    }

    private static IList<OpenApiParameter>? CreateParameters(
        IEnumerable<ParameterDescriptor> actionParameters,
        IList<OpenApiParameter> operationParameters,
        OperationFilterContext context)
    {
        var newParameters = actionParameters
            .Select(p => CreateParameter(p, operationParameters, context))
            .ToList();

        return newParameters.Any() ? newParameters : null;
    }

    private static OpenApiParameter CreateParameter(
        ParameterDescriptor actionParameter,
        IList<OpenApiParameter> operationParameters,
        OperationFilterContext context)
    {
        var operationParamNames = operationParameters.Select(p => p.Name);
        if (operationParamNames.Contains(actionParameter.Name))
            return operationParameters.First(p => p.Name == actionParameter.Name);

        var generatedSchema =
            context.SchemaGenerator.GenerateSchema(actionParameter.ParameterType, context.SchemaRepository);

        foreach (var param in operationParameters.Where(x => x.In == ParameterLocation.Path).Select(p => p.Name))
        {
            var properties = context.SchemaRepository.Schemas[actionParameter.ParameterType.Name]
                .Properties;
            var toRemove =
                properties.SingleOrDefault(x => string.Equals(x.Key, param, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(toRemove.Key))
                properties.Remove(toRemove);
        }

        var newParameter = new OpenApiParameter
        {
            Name = actionParameter.Name,
            In = ParameterLocation.Query,
            Schema = generatedSchema
        };

        return newParameter;
    }
}