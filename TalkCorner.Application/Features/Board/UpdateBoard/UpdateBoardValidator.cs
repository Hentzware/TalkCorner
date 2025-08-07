using FluentValidation;

namespace TalkCorner.Application.Features.Board.UpdateBoard;

public class UpdateBoardValidator : AbstractValidator<UpdateBoardCommand>
{
    public UpdateBoardValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Board Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.SortOrder)
            .NotEmpty().WithMessage("SortOrder is required.");
    }
}