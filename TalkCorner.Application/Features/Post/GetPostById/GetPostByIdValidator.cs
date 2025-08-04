using FluentValidation;

namespace TalkCorner.Application.Features.Post.GetPostById;

public class GetPostByIdValidator : AbstractValidator<GetPostByIdQuery>
{
    public GetPostByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must not be empty.");
    }
}