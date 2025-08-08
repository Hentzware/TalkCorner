using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Thread.StickThread;

public class StickThreadCommandHandler(IThreadRepository threadRepository) : IRequestHandler<StickThreadCommand, Unit>
{
    public async Task<Unit> Handle(StickThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = await threadRepository.GetThreadByIdWithTrackingAsync(request.Id);

        if (thread == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Thread), request.Id);
        }

        thread.Stick();

        await threadRepository.UpdateAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}