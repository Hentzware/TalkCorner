using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Thread.DeleteThread;

public class DeleteThreadCommandHandler(IThreadRepository threadRepository) : IRequestHandler<DeleteThreadCommand, Unit>
{
    public async Task<Unit> Handle(DeleteThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = await threadRepository.GetThreadByIdWithTrackingAsync(request.Id);

        if (thread == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Thread), request.Id);
        }

        await threadRepository.DeleteAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}