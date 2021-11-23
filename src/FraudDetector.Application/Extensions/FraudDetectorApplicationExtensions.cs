using System.Reflection;
using FluentValidation;
using FraudDetector.Application.Behaviours;
using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FraudDetector.Application.Extensions;

public static class FraudDetectorApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddApplicationServices()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

    private static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services.AddScoped<ISimilarityCalculator, SimilarityCalculator>();
}
