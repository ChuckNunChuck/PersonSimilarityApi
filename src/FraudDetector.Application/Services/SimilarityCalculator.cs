using System.Net.NetworkInformation;
using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Domain.Model;

namespace FraudDetector.Application.Services;

public class SimilarityCalculator : ISimilarityCalculator
{
    private const decimal PositiveResult = 1m;
    private const decimal NegativeResult = 0m;
    private const decimal SameLastNameProbability =0.4m;
    private const decimal SameFirstNameProbability = 0.3m;
    private const decimal SimilarFirstNameProbability = 0.15m;
    private const decimal SameDateOfBirthProbability = 0.4m;

    public decimal Calculate(Person person, SimilarPerson similarPerson) => 
        HasSameIdentification(person, similarPerson) 
            ? PositiveResult
            : CalculateBasedOnNameAndBirthDate(person, similarPerson);

    private decimal CalculateBasedOnNameAndBirthDate(Person person, SimilarPerson similarPerson)
    {
        if (!HasSameDateOfBirth(person, similarPerson))
            return NegativeResult;

        var notOccurProbability = PositiveResult;
        if (person.LastName.Equals(similarPerson.LastName, StringComparison.InvariantCulture))
            notOccurProbability *= SameLastNameProbability.InvertProbability();

        if (person.FirstName.Equals(similarPerson.FirstName, StringComparison.InvariantCulture))
            notOccurProbability *= SameFirstNameProbability.InvertProbability();

        if (HasSameDateOfBirth(person, similarPerson))
            notOccurProbability *= SameDateOfBirthProbability.InvertProbability();

        return notOccurProbability.InvertProbability();

    }

    private static bool HasSameDateOfBirth(Person person, SimilarPerson similarPerson) =>
        person.DateOfBirth != null
        && similarPerson.DateOfBirth != null
        && person.DateOfBirth == similarPerson.DateOfBirth;

    private static bool HasSameIdentification(Person person, SimilarPerson similarPerson) =>
        !string.IsNullOrEmpty(person.IdentificationNumber) 
        && !string.IsNullOrEmpty(similarPerson.IdentificationNumber) 
        && person.IdentificationNumber.Equals(similarPerson.IdentificationNumber,
            StringComparison.InvariantCulture);
}