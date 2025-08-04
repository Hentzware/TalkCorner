using FluentValidation;

namespace TalkCorner.Application.Features.User.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required.");
    }
}