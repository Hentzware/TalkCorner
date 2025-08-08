using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Thread.OpenThread;

public class OpenThreadCommandHandler(IThreadRepository threadRepository) : IRequestHandler<OpenThreadCommand, Unit>
{
    public async Task<Unit> Handle(OpenThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = await threadRepository.GetThreadByIdWithTrackingAsync(request.Id);

        if (thread == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Thread), request.Id);
        }

        thread.Open();

        await threadRepository.UpdateAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}