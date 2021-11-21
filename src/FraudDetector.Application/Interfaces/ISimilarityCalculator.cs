using FraudDetector.Application.Persons.Model;
using FraudDetector.Domain.Model;

namespace FraudDetector.Application.Interfaces;

public interface ISimilarityCalculator
{
    decimal Calculate(Person person, SimilarPerson similarPerson);
}