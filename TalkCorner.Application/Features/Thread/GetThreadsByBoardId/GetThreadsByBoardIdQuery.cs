using MediatR;

namespace TalkCorner.Application.Features.Thread.GetThreadsByBoardId;

public record GetThreadsByBoardIdQuery(Guid BoardId) : IRequest<IEnumerable<GetThreadsByBoardIdDto>>;