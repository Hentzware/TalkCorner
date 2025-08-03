using MediatR;

namespace TalkCorner.Application.Features.Post.GetPostByThreadId;

public record GetPostByThreadIdQuery(Guid ThreadId) : IRequest<IEnumerable<GetPostByThreadIdDto>>;