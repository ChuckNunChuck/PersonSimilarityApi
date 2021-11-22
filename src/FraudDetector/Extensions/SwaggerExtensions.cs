using System.Security.Claims;
using FraudDetector.Authorization;
using FraudDetector.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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

        options.UseLocalTokenProviderOptions(new[]
        {
            new Claim(Scopes.ClaimType, Scopes.Read),
            new Claim(Scopes.ClaimType, Scopes.Write)
        });
        options.OperationFilter<FromBodyAndRouteModelOperationFilter>();
    });

    public static SwaggerGenOptions UseLocalTokenProviderOptions(
        this SwaggerGenOptions options, 
        IEnumerable<Claim> claims)
    {
        options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = LocalJwtToken.GenerateJwtToken(claims)
        });

        options.AddSecurityRequirement(new()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                },
                new string[] {}
            }
        });

        return options;
    }
}