using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FraudDetector.Application.Enums;
using FraudDetector.Application.Interfaces;
using FraudDetector.Application.Persons.Commands.CalculateSimilarityCommand;
using FraudDetector.Application.Persons.Commands.CreatePersonCommand;
using FraudDetector.Application.Persons.Model;
using FraudDetector.Application.Tests.Database;
using FraudDetector.Domain.Model;
using FraudDetector.Infrastructure.Database;
using Moq;
using Xunit;

namespace FraudDetector.Application.Tests.Persons.Handlers;

public sealed class CalculateSimilarityCommandHandlerTests : IDisposable
{
    private readonly Fixture _fixture;
    private readonly FraudDetectorContext _context;
    private const decimal CalculatedProbability = 0.99m;

    public CalculateSimilarityCommandHandlerTests()
    {
        _context = InMemoryFraudDetectorContext.GetContext();
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());
        _fixture.Inject(_context);
        var similarityCalculatorMock = _fixture.Freeze<Mock<ISimilarityCalculator>>();
        similarityCalculatorMock.Setup(m =>
                m.Calculate(It.IsAny<Person>(), It.IsAny<SimilarPerson>()))
            .Returns(CalculatedProbability);
    }

    [Fact]
    public async Task HandlesSuccessfully()
    {
        var person = _fixture.Create<Person>();
        var personEntity = (await _context.AddAsync(person)).Entity;
        var command = _fixture
            .Build<CalculateSimilarityCommand>()
            .With(c => c.Id, personEntity.Id)
            .Create();
        var handler = _fixture.Create<CalculateSimilarityCommandHandler>();

        var result = await handler.Handle(command, CancellationToken.None);

        result.Result.Should().Be(CommandActionResult.Success);
        result.Value.Should().Be(CalculatedProbability);
    }

    [Fact]
    public async Task HandlesWithNotFoundResult()
    {
        var handler = _fixture.Create<CalculateSimilarityCommandHandler>();

        var result = await handler.Handle(
            _fixture.Create<CalculateSimilarityCommand>(), 
            CancellationToken.None);

        result.Result.Should().Be(CommandActionResult.NotFound);
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
