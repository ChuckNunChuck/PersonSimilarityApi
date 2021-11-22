using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FraudDetector.ConfigurationValidation;

[ExcludeFromCodeCoverage]
public static class ConfigurationValidationExtensions
{
    public static void ThrowOnInvalidConfiguration(this IConfiguration configuration,
        IEnumerable<AbstractValidator<IConfiguration>> validators)
    {
        var results =
            validators.Select(validator
                    => validator.Validate(configuration))
                .ToList();

        if (!results.All(result => result.IsValid))
        {
            throw new ServiceConfigurationException(
                results
                    .Select(result => result.ToString())
                    .Aggregate((current, next)
                        => $"{current}{Environment.NewLine}{next}"));
        }
    }
}