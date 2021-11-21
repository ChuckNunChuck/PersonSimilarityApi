using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Domain.Model;

namespace FraudDetector.Application.Services;

public class SimilarityCalculator : ISimilarityCalculator
{
    public decimal Calculate(Person person, SimilarPerson similarPerson)
    {
        throw new NotImplementedException();
    }
}