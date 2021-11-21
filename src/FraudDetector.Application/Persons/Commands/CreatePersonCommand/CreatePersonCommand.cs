using FraudDetector.Application.Model.Commands;
using MediatR;

namespace FraudDetector.Application.Persons.Commands.CreatePersonCommand;

public class CreatePersonCommand : PersonCommand, IRequest<CommandResult<Guid>>
{
}