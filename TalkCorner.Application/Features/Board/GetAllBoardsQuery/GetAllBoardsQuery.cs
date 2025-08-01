using MediatR;

namespace TalkCorner.Application.Features.Board.GetAllBoardsQuery;

public record GetAllBoardsQuery : IRequest<IEnumerable<GetAllBoardsDto>>;