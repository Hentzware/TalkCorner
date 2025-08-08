using FluentValidation;

namespace TalkCorner.Application.Features.Thread.StickThread;

public class StickThreadValidator : AbstractValidator<StickThreadCommand>
{
    public StickThreadValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}