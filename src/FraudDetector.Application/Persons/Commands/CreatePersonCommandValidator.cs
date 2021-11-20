using FluentValidation;

namespace FraudDetector.Application.Persons.Commands;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
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
    }
}