using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Post.UpdatePost;

public class UpdatePostCommandHandler(IPostRepository postRepository) : IRequestHandler<UpdatePostCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdWithTrackingAsync(request.Id);

        if (post == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Post), request.Id);
        }
        
        post.UpdateContent(request.Content);

        await postRepository.UpdateAsync(post, cancellationToken);
        await postRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}