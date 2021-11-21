namespace FraudDetector.Application.Persons.Model;

public record SimilarPerson(
    string? FirstName, 
    string? LastName, 
    DateTime? DateOfBirth, 
    string? IdentificationNumber);