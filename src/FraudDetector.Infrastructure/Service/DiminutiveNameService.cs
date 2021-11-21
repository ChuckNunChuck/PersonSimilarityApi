using FraudDetector.Infrastructure.Interfaces;

namespace FraudDetector.Infrastructure.Service;

public class DiminutiveNameService : IDiminutiveNameService
{
    private const string SourceResourceName = "names.csv";
    private const string SourceResourceFolder = "Resources";
    private Dictionary<string, string[]> DiminutiveNamesLookup { get; } = new();

    public DiminutiveNameService(IResourceReader resourceReader)
    {
        using var reader = new StreamReader(resourceReader.ReadEmbeddedResource(SourceResourceName, SourceResourceFolder));
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null)
                throw new InvalidOperationException($"Read null line from the resource {SourceResourceFolder}/{SourceResourceName}");

            var values = line.Split(',');
            if (values == null || values.Length < 2)
                throw new InvalidOperationException($"{SourceResourceName} doesn't use expected separator");

            DiminutiveNamesLookup.Add(values[0], values.Skip(1).ToArray());
        }
    }

    public bool IsDiminutiveName(string name, string otherName) => 
        IsKnownIsDiminutiveName(name, otherName) || IsKnownIsDiminutiveName(otherName, name);

    private bool IsKnownIsDiminutiveName(string name, string diminutiveName) => 
        DiminutiveNamesLookup.TryGetValue(name, out var result) 
        && result.Any(n => n.Equals(diminutiveName, StringComparison.InvariantCultureIgnoreCase));
}
