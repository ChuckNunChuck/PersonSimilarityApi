using AutoFixture;
using FluentAssertions;
using FraudDetector.Application.Enums;
using FraudDetector.Application.Persons.Commands.CreatePersonCommand;
using FraudDetector.Application.Tests.Database;
using FraudDetector.Domain.Model;
using FraudDetector.Infrastructure.Database;
using Xunit;

namespace FraudDetector.Application.Tests.Persons.Handlers;

public sealed class CreatePersonCommandHandlerTests : IDisposable
{
    private readonly Fixture _fixture;
    private readonly FraudDetectorContext _context;

    public CreatePersonCommandHandlerTests()
    {
        _context = InMemoryFraudDetectorContext.GetContext();
        _fixture = new Fixture();
        _fixture.Inject(_context);
    }

    [Fact]
    public async Task HandlesSuccessfully()
    {
        var command = _fixture.Create<CreatePersonCommand>();
        var handler = _fixture.Create<CreatePersonCommandHandler>();

        var result = await handler.Handle(command, CancellationToken.None);

        result.Result.Should().Be(CommandActionResult.Created);
        _context.Persons.Should().HaveCount(1);
        
        var person = _context.Persons.Single();
        result.Value.Should().Be(person.Id);
        EntityShouldBeEqualToCommand(person, command);
    }

    private static void EntityShouldBeEqualToCommand(
        Person person, 
        CreatePersonCommand command)
    {
        person.FirstName.Should().Be(command.FirstName);
        person.LastName.Should().Be(command.LastName);
        person.DateOfBirth.Should().Be(command.DateOfBirth!.Value.Date);
        person.IdentificationNumber.Should().Be(command.IdentificationNumber);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
