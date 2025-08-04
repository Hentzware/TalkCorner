using FluentValidation;

namespace TalkCorner.Application.Features.Board.GetBoardById;

public class GetBoardByIdValidator : AbstractValidator<GetBoardByIdQuery>
{
    public GetBoardByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must not be empty.");
    }
}