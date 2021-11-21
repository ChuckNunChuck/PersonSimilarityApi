namespace FraudDetector.Infrastructure.Interfaces;

public interface IResourceReader
{
    Stream ReadEmbeddedResource(string name, string folder);
}