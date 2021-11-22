namespace FraudDetector.Application.Settings;

public class PersonSimilarityWeights
{
    public decimal SameLastNameProbability { get; init; }
    public decimal SameFirstNameProbability { get; init; }
    public decimal SimilarFirstNameProbability { get; init; }
    public decimal SameDateOfBirthProbability { get; init; }
    public double SimilarFirstNameDistance { get; init; }
}