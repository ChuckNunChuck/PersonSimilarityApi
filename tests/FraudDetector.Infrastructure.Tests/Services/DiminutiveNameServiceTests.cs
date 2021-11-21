using FluentAssertions;
using FraudDetector.Infrastructure.Service;
using Xunit;

namespace FraudDetector.Infrastructure.Tests.Services;

public class DiminutiveNameServiceTests
{
    [Theory]
    [InlineData("Andrew", "Andy", true)]
    [InlineData("Andrew", "andy", true)]
    [InlineData("andrew", "Andy", true)]
    [InlineData("andrew", "andy", true)]
    [InlineData("Andy", "Andrew", true)]
    [InlineData("Andrew", "Dmytro", false)]
    public void CalculatesSimilarity(string name, string otherName, bool expectedResult)
    {
        var diminutiveNameService = new DiminutiveNameService(new ResourceReader());

        var result = diminutiveNameService.IsDiminutiveName(name, otherName);

        result.Should().Be(expectedResult);
    }
}