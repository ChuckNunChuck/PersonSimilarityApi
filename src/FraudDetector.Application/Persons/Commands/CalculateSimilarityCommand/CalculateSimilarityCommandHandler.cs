using FraudDetector.Application.Enums;
using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Model.Commands;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Infrastructure.Database;
using MediatR;

namespace FraudDetector.Application.Persons.Commands.CalculateSimilarityCommand;

public class CalculateSimilarityCommandHandler
    : IRequestHandler<CalculateSimilarityCommand, CommandResult<decimal>>
{
    private readonly FraudDetectorContext _context;
    private readonly ISimilarityCalculator _similarityCalculator;

    public CalculateSimilarityCommandHandler(
        FraudDetectorContext context,
        ISimilarityCalculator similarityCalculator)
    {
        _context = context;
        _similarityCalculator = similarityCalculator;
    }

    public async Task<CommandResult<decimal>> Handle(
        CalculateSimilarityCommand request, 
        CancellationToken cancellationToken)
    {
        var person = await _context.Persons.FindAsync(request.Id);
        return person == null
            ? CommandResult<decimal>.NotFound
            : new CommandResult<decimal>
        {
            Value = _similarityCalculator.Calculate(
                person,
                new SimilarPerson(
                    request.FirstName,
                    request.LastName,
                    request.DateOfBirth,
                    request.IdentificationNumber)),
            Result = CommandActionResult.Success
        };
    }
}