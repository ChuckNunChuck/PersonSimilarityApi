namespace FraudDetector.Infrastructure.Extensions;

public static class StreamReaderExtensions
{
    public static string[] GetSeparatedLineValues
        (this StreamReader reader, 
            char separator)
    {
        var values = reader.ReadLine()!.Split(separator);
        return values.Length < 2
            ? throw new InvalidOperationException($"Resource doesn't use expected separator {separator}")
            : values;
    }
}
