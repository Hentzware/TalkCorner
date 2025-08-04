using FluentValidation;

namespace TalkCorner.Application.Features.Thread.CreateThread;

public class CreateThreadValidator : AbstractValidator<CreateThreadCommand>
{
    public CreateThreadValidator()
    {
        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
    }
}