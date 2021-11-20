using System.Reflection;
using FluentValidation;
using FraudDetector.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FraudDetector.Application.Extensions;

public static class FraudDetectorApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        return services;
    }
}
