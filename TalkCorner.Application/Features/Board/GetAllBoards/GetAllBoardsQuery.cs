using MediatR;

namespace TalkCorner.Application.Features.Board.GetAllBoards;

public record GetAllBoardsQuery : IRequest<IEnumerable<GetAllBoardsDto>>;