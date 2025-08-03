using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Thread.CreateThread;

public class CreateThreadCommandHandler(IThreadRepository threadRepository, IMapper mapper) : IRequestHandler<CreateThreadCommand, Unit>
{
    public async Task<Unit> Handle(CreateThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = mapper.Map<Domain.Entities.Thread>(request);

        await threadRepository.AddAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}