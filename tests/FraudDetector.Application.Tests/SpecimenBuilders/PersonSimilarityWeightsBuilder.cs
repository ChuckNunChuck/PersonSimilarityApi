using System.Reflection;
using AutoFixture.Kernel;
using FraudDetector.Application.Settings;

namespace FraudDetector.Application.Tests.SpecimenBuilders;

internal sealed class PersonSimilarityWeightsBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        return request is TypeInfo seeded
               && seeded.FullName == typeof(PersonSimilarityWeights).FullName
            ? CreateDefault()
            : new NoSpecimen();
    }

    private static PersonSimilarityWeights CreateDefault() =>
        new()
        {
            SameLastNameProbability = 0.4m,
            SameFirstNameProbability = 0.2m,
            SimilarFirstNameProbability = 0.15m,
            SameDateOfBirthProbability = 0.4m,
            SimilarFirstNameDistance = 2
        };
}