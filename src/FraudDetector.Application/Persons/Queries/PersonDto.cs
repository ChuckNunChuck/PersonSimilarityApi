using FraudDetector.Domain.Model;

namespace FraudDetector.Application.Persons.Queries;

public class PersonDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string? IdentificationNumber { get; set; }
}