using AutoFixture;
using FluentAssertions;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Application.Services;
using FraudDetector.Domain.Model;
using Xunit;

namespace FraudDetector.Application.Tests.Services;

public class SimilarityCalculatorTests
{
    private readonly Fixture _fixture;

    public SimilarityCalculatorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void CalculatesSimilarity()
    {
        var person = _fixture.Create<Person>();
        var similarityCalculator = _fixture.Create<SimilarityCalculator>();

        var result = similarityCalculator.Calculate(
            person, 
            new SimilarPerson(person.FirstName, person.LastName, person.DateOfBirth, person.IdentificationNumber));

        result.Should().Be(1m);
    }
}