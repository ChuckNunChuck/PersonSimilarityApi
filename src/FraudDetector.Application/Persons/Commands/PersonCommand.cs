namespace FraudDetector.Application.Persons.Commands;

public class PersonCommand
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime? DateOfBirth { get; init; }
    public string? IdentificationNumber { get; init; }
}