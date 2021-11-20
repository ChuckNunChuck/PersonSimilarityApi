using FraudDetector.Domain.Common;

namespace FraudDetector.Domain.Model;

public class Person : AuditableEntity<Guid>
{
    public Person(string firstName, string lastName) 
        : base(Guid.Empty)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? IdentificationNumber { get; set; }
}
