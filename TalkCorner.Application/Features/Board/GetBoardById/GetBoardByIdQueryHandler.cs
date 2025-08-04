using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Board.GetBoardById;

public class GetBoardByIdQueryHandler(IBoardRepository boardRepository, IMapper mapper) : IRequestHandler<GetBoardByIdQuery, GetBoardByIdDto>
{
    public async Task<GetBoardByIdDto> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
    {
        var board = await boardRepository.GetBoardByIdAsync(request.Id);
        var response = mapper.Map<GetBoardByIdDto>(board);

        return response;
    }
}