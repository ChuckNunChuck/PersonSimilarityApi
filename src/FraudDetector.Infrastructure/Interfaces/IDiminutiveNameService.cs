namespace FraudDetector.Infrastructure.Interfaces;

public interface IDiminutiveNameService
{
    bool IsDiminutiveName(string name, string otherName);
}