using MediatR;

namespace TalkCorner.Application.Features.Post.GetPostById;

public record GetPostByIdQuery(Guid Id) : IRequest<GetPostByIdDto>;