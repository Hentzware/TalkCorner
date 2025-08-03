using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Post.CreatePost;

public class CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper) : IRequestHandler<CreatePostCommand, Unit>
{
    public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = mapper.Map<Domain.Entities.Post>(request);

        await postRepository.AddAsync(post, cancellationToken);
        await postRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}