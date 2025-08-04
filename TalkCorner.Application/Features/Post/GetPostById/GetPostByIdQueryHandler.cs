using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Post.GetPostById;

public class GetPostByIdQueryHandler(IPostRepository postRepository, IMapper mapper) : IRequestHandler<GetPostByIdQuery, GetPostByIdDto>
{
    public async Task<GetPostByIdDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdWithTrackingAsync(request.Id);

        if (post == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Post), request.Id);
        }

        var response = mapper.Map<GetPostByIdDto>(post);

        return response;
    }
}