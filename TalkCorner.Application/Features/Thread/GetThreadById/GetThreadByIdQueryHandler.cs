using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Thread.GetThreadById;

public class GetThreadByIdQueryHandler(IThreadRepository threadRepository, IMapper mapper) : IRequestHandler<GetThreadByIdQuery, GetThreadByIdDto>
{
    public async Task<GetThreadByIdDto> Handle(GetThreadByIdQuery request, CancellationToken cancellationToken)
    {
        var thread = await threadRepository.GetThreadByIdAsync(request.Id);
        
        if (thread == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Thread), request.Id);
        }

        var response = mapper.Map<GetThreadByIdDto>(thread);

        return response;
    }
}