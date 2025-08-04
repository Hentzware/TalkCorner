using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Post.DeletePost;

public class DeletePostCommandHandler(IPostRepository postRepository, IMapper mapper) : IRequestHandler<DeletePostCommand, Unit>
{
    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdWithTrackingAsync(request.Id);

        if (post == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Post), request.Id);
        }

        await postRepository.DeleteAsync(post, cancellationToken);
        await postRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}