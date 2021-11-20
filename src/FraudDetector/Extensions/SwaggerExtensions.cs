using FraudDetector.Filters;
using Microsoft.OpenApi.Models;

namespace FraudDetector.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(
        this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Fraud Detection API",
            Description = "Docs for Fraud Detection API",
            Version = "v1"
        });

        options.OperationFilter<FromBodyAndRouteModelOperationFilter>();
    });
}