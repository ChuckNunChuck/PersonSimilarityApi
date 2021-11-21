using FraudDetector.Infrastructure.Extensions;
using FraudDetector.Infrastructure.Interfaces;

namespace FraudDetector.Infrastructure.Service;

public class DiminutiveNameService : IDiminutiveNameService
{
    private const string SourceResourceName = "names.csv";
    private const string SourceResourceFolder = "Resources";
    private const char Separator = ',';
    private Dictionary<string, string[]> DiminutiveNamesLookup { get; } = new();

    public DiminutiveNameService(IResourceReader resourceReader)
    {
        using var reader = new StreamReader(
            resourceReader.ReadEmbeddedResource(SourceResourceName, SourceResourceFolder));
        while (!reader.EndOfStream)
        {
            var values = reader.GetSeparatedLineValues(Separator);
            DiminutiveNamesLookup.Add(values[0].ToUpperInvariant(), values.Skip(1).ToArray());
        }
    }

    public bool IsDiminutiveName(string name, string otherName) => 
        IsKnownIsDiminutiveName(name, otherName) 
        || IsKnownIsDiminutiveName(otherName, name);

    private bool IsKnownIsDiminutiveName(string name, string diminutiveName) => 
        DiminutiveNamesLookup.TryGetValue(name.ToUpperInvariant(), out var result) 
        && result.Any(n => 
            n.Equals(diminutiveName, StringComparison.InvariantCultureIgnoreCase));

}
