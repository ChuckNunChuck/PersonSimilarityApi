namespace FraudDetector.ConfigurationValidation.Validators;

internal sealed class PersonSimilarityWeightsValidator : AbstractConfigurationValidator
{
    public PersonSimilarityWeightsValidator() : base(new[]
    {
        "PersonSimilarityWeights:SameLastNameProbability",
        "PersonSimilarityWeights:SameFirstNameProbability",
        "PersonSimilarityWeights:SimilarFirstNameProbability",
        "PersonSimilarityWeights:SameDateOfBirthProbability",
        "PersonSimilarityWeights:SimilarFirstNameDistance"
    })
    { }
}