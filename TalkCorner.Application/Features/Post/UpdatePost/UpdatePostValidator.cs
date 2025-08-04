using FluentValidation;

namespace TalkCorner.Application.Features.Post.UpdatePost;

public class UpdatePostValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must not be empty.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content must not be empty.")
            .MinimumLength(3).WithMessage("Content must be at least 3 characters long.")
            .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");
    }
}