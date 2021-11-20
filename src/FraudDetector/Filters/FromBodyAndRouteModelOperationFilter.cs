using FraudDetector.ModelBinding.BodyAndRoute;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FraudDetector.Filters;

public class FromBodyAndRouteModelOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var description = context.ApiDescription;
        if (description.HttpMethod?.ToLower() == HttpMethod.Get.ToString()
                .ToLower())
            return;

        var actionParameters = description.ActionDescriptor.Parameters.OfType<ControllerParameterDescriptor>()
            .Where(p =>
                p.ParameterInfo.CustomAttributes.Any(a =>
                    a.AttributeType == typeof(FromBodyAndRouteAttribute)))
            .ToList();
        if (!actionParameters.Any())
            return;

        var apiParameters = description.ParameterDescriptions
            .Where(p => p.Source.IsFromRequest)
            .ToList();

        WalkOperation(operation, context, actionParameters, apiParameters);
    }

    private static void WalkOperation(OpenApiOperation operation,
        OperationFilterContext context,
        IReadOnlyCollection<ControllerParameterDescriptor> actionParameters,
        IReadOnlyCollection<ApiParameterDescription> apiParameters)
    {
        foreach (var actionParameter in actionParameters.IntersectBy(context.SchemaRepository.Schemas.Keys,
                     x => x.ParameterType.Name, StringComparer.OrdinalIgnoreCase))
        {

            foreach (var property in context
                         .SchemaRepository.Schemas[actionParameter.ParameterType.Name]
                         .Properties.IntersectBy(apiParameters.Select(x => x.Name), x => x.Key,
                             StringComparer.OrdinalIgnoreCase))
            {

                context.SchemaRepository.Schemas[actionParameter.ParameterType.Name]
                    .Properties.Remove(property);
                operation.Parameters.Remove(operation.Parameters.Single(p =>
                    string.Equals(p.Name, actionParameter.Name, StringComparison.OrdinalIgnoreCase)));
            }

            if (!context.SchemaRepository.Schemas[actionParameter.ParameterType.Name]
                    .Properties.Any())
            {
                context.SchemaRepository.Schemas.Remove(actionParameter.ParameterType.Name);
                operation.RequestBody = null;
                continue;
            }

            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = context.SchemaGenerator.GenerateSchema(actionParameter.ParameterType,
                            context.SchemaRepository)
                    }
                }
            };
        }
    }
}