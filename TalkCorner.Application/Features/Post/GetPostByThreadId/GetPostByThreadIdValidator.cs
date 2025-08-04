using FluentValidation;

namespace TalkCorner.Application.Features.Post.GetPostByThreadId;

public class GetPostByThreadIdValidator : AbstractValidator<GetPostByThreadIdQuery>
{
    public GetPostByThreadIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ThreadId must not be empty.");
    }
}