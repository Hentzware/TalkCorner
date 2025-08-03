using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Thread.GetThreadsByBoardId;

public class GetThreadsByBoardIdQueryHandler(IThreadRepository threadRepository, IMapper mapper) : IRequestHandler<GetThreadsByBoardIdQuery, IEnumerable<GetThreadsByBoardIdDto>>
{
    public async Task<IEnumerable<GetThreadsByBoardIdDto>> Handle(GetThreadsByBoardIdQuery request, CancellationToken cancellationToken)
    {
        var threads = await threadRepository.GetThreadsByBoardIdAsync(request.BoardId);
        var response = mapper.Map<IEnumerable<GetThreadsByBoardIdDto>>(threads);
        return response;
    }
}