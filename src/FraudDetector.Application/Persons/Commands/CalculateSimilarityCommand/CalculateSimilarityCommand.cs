using FraudDetector.Application.Model.Commands;
using MediatR;

namespace FraudDetector.Application.Persons.Commands;

public class CalculateSimilarityCommand : IRequest<CommandResult<decimal>>
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? IdentificationNumber { get; init; }
}