using FraudDetector.Application.Enums;
using FraudDetector.Application.Model.Commands;
using FraudDetector.Infrastructure.Database;
using MediatR;

namespace FraudDetector.Application.Persons.Commands;

public class CalculateSimilarityCommandHandler
    : IRequestHandler<CalculateSimilarityCommand, CommandResult<decimal>>
{
    private readonly FraudDetectorContext _context;

    public CalculateSimilarityCommandHandler(FraudDetectorContext context)
    {
        _context = context;
    }

    public Task<CommandResult<decimal>> Handle(
        CalculateSimilarityCommand request, 
        CancellationToken cancellationToken) =>
       Task.FromResult(new CommandResult<decimal>
       {
           Value = 0m,
           Result = CommandActionResult.Success
       });
}