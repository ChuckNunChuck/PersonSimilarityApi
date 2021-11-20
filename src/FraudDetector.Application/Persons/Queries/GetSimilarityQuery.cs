using MediatR;

namespace FraudDetector.Application.Persons.Queries;

public class GetSimilarityQuery : IRequest<decimal>
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? IdentificationNumber { get; init; }
}