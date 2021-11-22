using AutoFixture;

namespace FraudDetector.Application.Tests.SpecimenBuilders;

internal static class Extensions
{
    public static void AddSimilarityCalculationSpecimenBuilders(this Fixture fixture)
    {
        fixture.Customizations.Add(new PersonSimilarityWeightsBuilder());
    }
}