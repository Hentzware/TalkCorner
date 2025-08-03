using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Thread.UpdateThread;

public class UpdateThreadCommandHandler(IThreadRepository threadRepository, IMapper mapper) : IRequestHandler<UpdateThreadCommand, Unit>
{
    public async Task<Unit> Handle(UpdateThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = await threadRepository.GetThreadByIdWithTrackingAsync(request.Id);

        if (thread == null)
        {
            throw new InvalidOperationException("Thread does not exist.");
        }
        
        thread.UpdateTitle(request.Title);

        await threadRepository.UpdateAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}