using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Domain.Model;

namespace FraudDetector.Application.Services;

public class SimilarityCalculator : ISimilarityCalculator
{
    public decimal Calculate(Person person, SimilarPerson similarPerson)
    {
        if (HasSameIdentification(person, similarPerson))
            return 1m;

        return 0;
    }

    private static bool HasSameIdentification(Person person, SimilarPerson similarPerson) =>
        !string.IsNullOrEmpty(person.IdentificationNumber) 
        && !string.IsNullOrEmpty(similarPerson.IdentificationNumber) 
        && person.IdentificationNumber.Equals(similarPerson.IdentificationNumber,
            StringComparison.InvariantCulture);
}