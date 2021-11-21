using FraudDetector.Application.Model.Commands;
using MediatR;

namespace FraudDetector.Application.Persons.Commands.CalculateSimilarityCommand;

public class CalculateSimilarityCommand : PersonCommand, IRequest<CommandResult<decimal>> 
{
    public Guid Id { get; init; }
}