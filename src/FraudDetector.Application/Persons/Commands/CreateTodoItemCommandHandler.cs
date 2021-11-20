using FraudDetector.Application.Common.Enums;
using FraudDetector.Application.Common.Models.Commands;
using FraudDetector.Domain.Model;
using FraudDetector.Infrastructure.Database;
using MediatR;

namespace FraudDetector.Application.Persons.Commands;

public class CreateTodoItemCommandHandler : IRequestHandler<CreatePersonCommand, CommandResult<Guid>>
{
    private readonly FraudDetectorContext _context;

    public CreateTodoItemCommandHandler(FraudDetectorContext context)
    {
        _context = context;
    }

    public async Task<CommandResult<Guid>> Handle(
        CreatePersonCommand request, 
        CancellationToken cancellationToken)
    {
        var person = await _context.Persons.AddAsync(
            new Person(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.IdentificationNumber),
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new CommandResult<Guid>
        {
            Value = person.Entity.Id,
            Result = CommandActionResult.Created
        };
    }
}