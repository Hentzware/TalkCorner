using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Post.GetPostByThreadId;

public class GetPostByThreadIdQueryHandler(IPostRepository postRepository, IMapper mapper) : IRequestHandler<GetPostByThreadIdQuery, IEnumerable<GetPostByThreadIdDto>>
{
    public async Task<IEnumerable<GetPostByThreadIdDto>> Handle(GetPostByThreadIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await postRepository.GetPostsByThreadIdAsync(request.Id);
        var response = mapper.Map<IEnumerable<GetPostByThreadIdDto>>(posts);

        return response;
    }
}