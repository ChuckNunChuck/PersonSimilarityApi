using FluentValidation;

namespace FraudDetector.Application.Persons.Commands;

public class PersonCommandValidator<T> : AbstractValidator<T>
    where T: PersonCommand
{
    public PersonCommandValidator()
    {
        RuleFor(person => person.FirstName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(person => person.LastName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(person => person.IdentificationNumber)
            .MaximumLength(255)
            .When(person => person.IdentificationNumber != null);

        RuleFor(person => person.DateOfBirth)
            .Must(date => date > new DateTime(1900, 1, 1))
            .When(person => person.DateOfBirth != null)
            .WithMessage("Date of birth year must be bigger than 1900");
    }
}