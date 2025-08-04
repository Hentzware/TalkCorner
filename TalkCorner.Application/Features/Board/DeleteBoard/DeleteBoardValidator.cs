using FluentValidation;

namespace TalkCorner.Application.Features.Board.DeleteBoard;

public class DeleteBoardValidator : AbstractValidator<DeleteBoardCommand>
{
    public DeleteBoardValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty().WithMessage("Board Id is required.");

    }
}