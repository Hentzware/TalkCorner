using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Board.GetAllBoardsQuery;

public class GetAllBoardsQueryHandler(IBoardRepository boardRepository, IMapper mapper) : IRequestHandler<GetAllBoardsQuery, IEnumerable<GetAllBoardsDto>>
{
    public async Task<IEnumerable<GetAllBoardsDto>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
    {
        var boards = await boardRepository.GetAsync();
        var response = mapper.Map<IEnumerable<GetAllBoardsDto>>(boards);
        return response;
    }
}