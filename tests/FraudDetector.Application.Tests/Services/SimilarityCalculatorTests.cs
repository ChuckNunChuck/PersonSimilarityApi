using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Application.Services;
using FraudDetector.Application.Tests.SpecimenBuilders;
using FraudDetector.Domain.Model;
using Xunit;

namespace FraudDetector.Application.Tests.Services;

public class SimilarityCalculatorTests
{
    private readonly Fixture _fixture;

    public SimilarityCalculatorTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());
        _fixture.AddSimilarityCalculationSpecimenBuilders();
    }

    [Theory]
    [MemberData(nameof(Persons))]
    public void CalculatesSimilarity(
        Person person, 
        SimilarPerson similarPerson, 
        decimal expectedProbability)
    {
        var similarityCalculator = _fixture.Create<SimilarityCalculator>();

        var result = similarityCalculator.Calculate(person, similarPerson);

        result.Should().Be(expectedProbability);
    }

    public static IEnumerable<object[]> Persons()
    {
        return new[]
        {
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), null),
                new SimilarPerson("Andrew", "Craw", null, null),
                0.52
            },
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), null),
                new SimilarPerson("Petty", "Smith", new DateTime(1985, 2, 20), null),
                0.4
            },
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), null),
                new SimilarPerson("A.", "Craw", new DateTime(1985, 2, 20), null),
                0.694
            },
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), "931212312"),
                new SimilarPerson("Petty", "Smith", new DateTime(1985, 2, 20), "931212312"),
                1.0
            },
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), "931212312"),
                new SimilarPerson("Andew", "Snow", null, null),
                0.15
            },
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), "931212312"),
                new SimilarPerson("Andrev", "Snow", null, null),
                0.15
            },
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), "931212312"),
                new SimilarPerson("Andrev", "Craw", null, null),
                0.49
            },
            new object[]
            {
                new Person("Andrew", "Craw", new DateTime(1985, 2, 20), "931212312"),
                new SimilarPerson("Andrxx", "Snow", null, null),
                0.0
            }
        };
    }
}