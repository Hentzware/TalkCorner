using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Post.UpdatePost;

public class UpdatePostCommandHandler(IPostRepository postRepository, IMapper mapper) : IRequestHandler<UpdatePostCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdWithTrackingAsync(request.Id);

        if (post == null)
        {
            throw new InvalidOperationException("Post does not exist.");
        }
        
        post.UpdateContent(request.Content);

        await postRepository.UpdateAsync(post, cancellationToken);
        await postRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}