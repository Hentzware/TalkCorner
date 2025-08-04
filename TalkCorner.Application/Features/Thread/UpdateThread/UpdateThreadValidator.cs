using FluentValidation;

namespace TalkCorner.Application.Features.Thread.UpdateThread;

public class UpdateThreadValidator : AbstractValidator<UpdateThreadCommand>
{
    public UpdateThreadValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Thread Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
    }
}