using FluentValidation;

namespace TalkCorner.Application.Features.Thread.UnstickThread;

public class UnstickThreadValidator : AbstractValidator<UnstickThreadCommand>
{
    public UnstickThreadValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}