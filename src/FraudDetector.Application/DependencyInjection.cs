using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FraudDetector.Application.Behaviours;
using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Services;

namespace FraudDetector.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IPipelineBehavior<,>), 
                typeof(UnhandledExceptionBehaviour<,>))
            .AddApplicationServices();

    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services.AddScoped<ISimilarityCalculator, SimilarityCalculator>();
}