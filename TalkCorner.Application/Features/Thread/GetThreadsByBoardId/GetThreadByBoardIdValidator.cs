using FluentValidation;

namespace TalkCorner.Application.Features.Thread.GetThreadsByBoardId;

public class GetThreadByBoardIdValidator : AbstractValidator<GetThreadsByBoardIdQuery>
{
    public GetThreadByBoardIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Board Id is required.");
    }
}