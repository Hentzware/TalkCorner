using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Thread.UnstickThread;

public class UnstickThreadCommandHandler(IThreadRepository threadRepository) : IRequestHandler<UnstickThreadCommand, Unit>
{
    public async Task<Unit> Handle(UnstickThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = await threadRepository.GetThreadByIdWithTrackingAsync(request.Id);

        if (thread == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Thread), request.Id);
        }

        thread.Unstick();

        await threadRepository.UpdateAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}