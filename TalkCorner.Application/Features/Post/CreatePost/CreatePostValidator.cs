using FluentValidation;

namespace TalkCorner.Application.Features.Post.CreatePost;

public class CreatePostValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.ThreadId)
            .NotEmpty()
            .WithMessage("ThreadId must not be empty.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content must not be empty.")
            .MinimumLength(3).WithMessage("Content must be at least 3 characters long.")
            .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");

        RuleFor(x => x.ParentPostId)
            .Must(pid => !pid.HasValue || pid.Value != Guid.Empty)
            .WithMessage("ParentPostId must not be Guid.Empty if provided.");
    }
}