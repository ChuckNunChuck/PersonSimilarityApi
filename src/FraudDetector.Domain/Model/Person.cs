using FraudDetector.Domain.Common;

namespace FraudDetector.Domain.Model;

public class Person : AuditableEntity<Guid>
{
    public Person(
        string firstName, 
        string lastName, 
        DateTime? dateOfBirth,
        string? identificationNumber) 
        : base(Guid.Empty)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth?.Date;
        IdentificationNumber = identificationNumber;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public DateTime? DateOfBirth { get; }
    public string? IdentificationNumber { get; }
}
