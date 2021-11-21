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

    private static decimal CalculateBasedOnNameAndBirthDate(Person person, SimilarPerson similarPerson)
    {
        if (!HasSameDateOfBirth(person, similarPerson))
            return NegativeResult;

        var notOccurProbability = PositiveResult;

        notOccurProbability *= FirstNameNotOccurProbability(person, similarPerson);
        notOccurProbability *= LastNameNotOccurProbability(person, similarPerson);
        notOccurProbability *= DateOfBirthNotOccurProbability(person, similarPerson);

        return notOccurProbability.InvertProbability();
    }
    private static decimal FirstNameNotOccurProbability(Person person, SimilarPerson similarPerson)
    {
        if (person.FirstName.Equals(similarPerson.FirstName, StringComparison.InvariantCulture))
        {
            return SameFirstNameProbability.InvertProbability();
        }

        return HasSimilarFirstName(person.FirstName, similarPerson.FirstName)
            ? SimilarFirstNameProbability.InvertProbability()
            : PositiveResult;
    }

    private static decimal LastNameNotOccurProbability(Person person, SimilarPerson similarPerson) =>
        person.LastName.Equals(similarPerson.LastName, StringComparison.InvariantCulture)
            ? SameLastNameProbability.InvertProbability()
            : PositiveResult;

    private static decimal DateOfBirthNotOccurProbability(Person person, SimilarPerson similarPerson) =>
        HasSameDateOfBirth(person, similarPerson) 
            ? SameDateOfBirthProbability.InvertProbability() 
            : PositiveResult;

    private static bool HasSimilarFirstName(string personFirstName, string similarPersonFirstName)
    {
        throw new NotImplementedException();
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