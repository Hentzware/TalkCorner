using FluentValidation;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Application.Features.Authentication.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IApplicationUserRepository applicationUserRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-Mail is required.")
            .EmailAddress()
            .WithMessage("Must be a valid E-Mail address.")
            .MustAsync(async (email, cancellation) => !await applicationUserRepository.EmailExistsAsync(email))
            .WithMessage("E-Mail is already taken.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 chars long.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one upper case char.")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lower case char.")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .WithMessage("DisplayName is required.")
            .Length(DisplayName.MinLength, DisplayName.MaxLength)
            .WithMessage($"DisplayName must be between {DisplayName.MinLength} and {DisplayName.MaxLength} characters long.")
            .Matches("^[a-zA-Z0-9_-]+$")
            .WithMessage("DisplayName may only contain letters, digits, underscores and dashes.");
    }
}