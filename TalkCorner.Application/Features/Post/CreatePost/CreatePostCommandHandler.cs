using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Post.CreatePost;

public class CreatePostCommandHandler(IPostRepository postRepository) : IRequestHandler<CreatePostCommand, Unit>
{
    public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = Domain.Entities.Post.Create(request.Content, request.CurrentUserId, request.ThreadId, request.ParentPostId);

        await postRepository.AddAsync(post, cancellationToken);
        await postRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}