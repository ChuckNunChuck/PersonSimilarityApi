using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace FraudDetector.ConfigurationValidation;

[ExcludeFromCodeCoverage]
internal abstract class AbstractConfigurationValidator : AbstractValidator<IConfiguration>
{
    protected AbstractConfigurationValidator(IEnumerable<string> configurationKeys)
    {
        foreach (var configurationKey in configurationKeys)
        {
            RuleFor(configuration => configuration[configurationKey])
                .NotEmpty()
                .WithName(configurationKey);
        }
    }
}