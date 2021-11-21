namespace FraudDetector.Application.Services;

public static class ProbabilityExtensions
{
    public static decimal InvertProbability(this decimal probability) => 
        1 - probability;
}
