using System.Reflection;
using FraudDetector.Infrastructure.Interfaces;

namespace FraudDetector.Infrastructure.Service;

public class ResourceReader : IResourceReader
{
    public Stream ReadEmbeddedResource(string name, string folder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly
            .GetManifestResourceStream(
                $"{assembly.FullName!.Split(',')[0]}.{folder}.{name}");

        if (stream == null)
        {
            throw new InvalidOperationException($"Resource {name} is missed");
        }

        return stream;
    }
}