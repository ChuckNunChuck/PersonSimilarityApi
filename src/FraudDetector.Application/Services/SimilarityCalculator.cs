using F23.StringSimilarity;
using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Application.Settings;
using FraudDetector.Domain.Model;
using FraudDetector.Infrastructure.Interfaces;

namespace FraudDetector.Application.Services;

public class SimilarityCalculator : ISimilarityCalculator
{
    private readonly IDiminutiveNameService _diminutiveNameService;
    private readonly PersonSimilarityWeights _personSimilarityWeights;
    private const decimal OneProbability = 1m;
    private const decimal ZeroProbability = 0m;

    public SimilarityCalculator(
        IDiminutiveNameService diminutiveNameService, 
        PersonSimilarityWeights personSimilarityWeights)
    {
        _diminutiveNameService = diminutiveNameService;
        _personSimilarityWeights = personSimilarityWeights;
    }

    public decimal Calculate(Person person, SimilarPerson similarPerson) => 
        HasSameIdentification(person, similarPerson) 
            ? OneProbability
            : CalculateBasedOnNameAndBirthDate(person, similarPerson);

    private decimal CalculateBasedOnNameAndBirthDate(Person person, SimilarPerson similarPerson)
    {
        if (person.DateOfBirth != null 
            && similarPerson.DateOfBirth != null
            && !HasSameDateOfBirth(person, similarPerson))
            return ZeroProbability;

        var notOccurProbability = OneProbability;

        notOccurProbability *= FirstNameNotOccurProbability(person, similarPerson);
        notOccurProbability *= LastNameNotOccurProbability(person, similarPerson);
        notOccurProbability *= DateOfBirthNotOccurProbability(person, similarPerson);

        return notOccurProbability.InvertProbability();
    }
    private decimal FirstNameNotOccurProbability(Person person, SimilarPerson similarPerson)
    {
        if (person.FirstName.Equals(similarPerson.FirstName, StringComparison.InvariantCultureIgnoreCase))
        {
            return _personSimilarityWeights.SameFirstNameProbability.InvertProbability();
        }

        return HasSimilarFirstName(person.FirstName, similarPerson.FirstName)
            ? _personSimilarityWeights.SimilarFirstNameProbability.InvertProbability()
            : OneProbability;
    }

    private bool HasSimilarFirstName(string personFirstName, string similarPersonFirstName) => 
        new Damerau().Distance(personFirstName, similarPersonFirstName) 
        <= _personSimilarityWeights.SimilarFirstNameDistance
        || ContainsInitials(personFirstName, similarPersonFirstName)
        || _diminutiveNameService.IsDiminutiveName(personFirstName, similarPersonFirstName);

    private static bool ContainsInitials(string personFirstName, string similarPersonFirstName) =>
        IsInitial(personFirstName) || IsInitial(similarPersonFirstName)
        && char.ToUpperInvariant(personFirstName[0]) == char.ToUpperInvariant(similarPersonFirstName[0]);

    private static bool IsInitial(string str) => 
        str.Length == 1 || (str.Length == 2 && str[1] == '.');

    private decimal LastNameNotOccurProbability(Person person, SimilarPerson similarPerson) =>
        person.LastName.Equals(similarPerson.LastName, StringComparison.InvariantCultureIgnoreCase)
            ? _personSimilarityWeights.SameLastNameProbability.InvertProbability()
            : OneProbability;

    private decimal DateOfBirthNotOccurProbability(Person person, SimilarPerson similarPerson) =>
        HasSameDateOfBirth(person, similarPerson) 
            ? _personSimilarityWeights.SameDateOfBirthProbability.InvertProbability() 
            : OneProbability;

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