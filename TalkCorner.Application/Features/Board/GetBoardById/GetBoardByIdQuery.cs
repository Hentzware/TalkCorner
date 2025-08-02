using MediatR;

namespace TalkCorner.Application.Features.Board.GetBoardById;

public record GetBoardByIdQuery(Guid Id) : IRequest<GetBoardByIdDto>;