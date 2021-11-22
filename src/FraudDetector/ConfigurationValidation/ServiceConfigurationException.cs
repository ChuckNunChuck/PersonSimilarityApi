using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FraudDetector.ConfigurationValidation;

[ExcludeFromCodeCoverage]
[Serializable]
public class ServiceConfigurationException : Exception
{
    public ServiceConfigurationException()
    {
    }

    public ServiceConfigurationException(string message, Exception inner) : base(message, inner)
    {
    }

    public ServiceConfigurationException(string validationErrors) : base(validationErrors)
    {
    }

    protected ServiceConfigurationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}