using FluentValidation;

namespace TalkCorner.Application.Features.User.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must not be empty.");

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .WithMessage("DisplayName must not be empty.")
            .MaximumLength(32)
            .WithMessage("DisplayName must not exceed 32 characters.");
    }
}