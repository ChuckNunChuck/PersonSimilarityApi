using AutoFixture;
using FluentAssertions;
using FraudDetector.Application.Persons.Commands;
using Xunit;

namespace FraudDetector.Application.Tests.Validators;

public class PersonCommandValidatorTests
{
    [Theory]
    [MemberData(nameof(Commands))]
    public void Validates(PersonCommand command, List<string> errors)
    {
        var result = new PersonCommandValidator<PersonCommand>()
            .Validate(command);

        result.IsValid.Should()
            .Be(!errors.Any());

        if (errors.Any())
        {
            result.Errors.Select(e => e.ErrorMessage)
                .Should().Contain(errors);
        }
    }

    public static IEnumerable<object[]> Commands()
    {
        var fixture = new Fixture();
        return new[]
        {
            new object[]
            {
                new PersonCommand
                {
                    FirstName = fixture.Create<string>(),
                    LastName = fixture.Create<string>(),
                },
                new List<string>()
            },
            new object[]
            {
                new PersonCommand(),
                new List<string>
                {
                    "'First Name' must not be empty.",
                    "'Last Name' must not be empty."
                }
            },
            new object[]
            {
                new PersonCommand
                {
                    FirstName = new string('A', 51),
                    LastName = new string('A', 51),
                    IdentificationNumber = new string('A', 256),
                    DateOfBirth = new DateTime(1899, 12, 12)
                },
                new List<string>
                {
                    "The length of 'First Name' must be 50 characters or fewer. You entered 51 characters.",
                    "The length of 'Last Name' must be 50 characters or fewer. You entered 51 characters.",
                    "The length of 'Identification Number' must be 255 characters or fewer. You entered 256 characters.",
                    "Date of birth year must be bigger than 1900"
                }
            }
        };
    }
}
