using MediatR;

namespace TalkCorner.Application.Features.Thread.GetThreadById;

public record GetThreadByIdQuery(Guid Id) : IRequest<GetThreadByIdDto>;