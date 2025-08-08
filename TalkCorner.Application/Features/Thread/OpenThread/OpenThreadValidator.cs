using FluentValidation;

namespace TalkCorner.Application.Features.Thread.OpenThread;

public class OpenThreadValidator : AbstractValidator<OpenThreadCommand>
{
    public OpenThreadValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}