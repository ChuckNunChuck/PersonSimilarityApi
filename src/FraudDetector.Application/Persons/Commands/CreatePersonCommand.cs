using FraudDetector.Application.Common.Models.Commands;
using MediatR;

namespace FraudDetector.Application.Persons.Commands;

public class CreatePersonCommand : IRequest<CommandResult<Guid>>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime? DateOfBirth { get; init; }
    public string? IdentificationNumber { get; init; }
}