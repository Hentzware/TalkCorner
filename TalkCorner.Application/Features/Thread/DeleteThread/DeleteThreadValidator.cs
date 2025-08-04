using FluentValidation;

namespace TalkCorner.Application.Features.Thread.DeleteThread;

public class DeleteThreadValidator : AbstractValidator<DeleteThreadCommand>
{
    public DeleteThreadValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Thread Id is required.");
    }
}