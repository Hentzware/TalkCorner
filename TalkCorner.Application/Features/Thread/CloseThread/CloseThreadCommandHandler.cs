using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Thread.CloseThread;

public class CloseThreadCommandHandler(IThreadRepository threadRepository) : IRequestHandler<CloseThreadCommand, Unit>
{
    public async Task<Unit> Handle(CloseThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = await threadRepository.GetThreadByIdWithTrackingAsync(request.Id);

        if (thread == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Thread), request.Id);
        }

        thread.Close();

        await threadRepository.UpdateAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}