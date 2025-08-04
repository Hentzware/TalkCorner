using FluentValidation;

namespace TalkCorner.Application.Features.Post.DeletePost;

public class DeletePostValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must not be empty.");
    }
}