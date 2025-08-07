using FluentValidation;

namespace TalkCorner.Application.Features.Thread.CloseThread;

public class CloseThreadValidator : AbstractValidator<CloseThreadCommand>
{
    public CloseThreadValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}