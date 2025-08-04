using FluentValidation;

namespace TalkCorner.Application.Features.Thread.GetThreadById;

public class GetThreadByIdValidator : AbstractValidator<GetThreadByIdQuery>
{
    public GetThreadByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Thread Id is required.");
    }
}