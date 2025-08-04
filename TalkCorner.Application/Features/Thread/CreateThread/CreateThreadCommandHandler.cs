using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Thread.CreateThread;

public class CreateThreadCommandHandler(IThreadRepository threadRepository) : IRequestHandler<CreateThreadCommand, Unit>
{
    public async Task<Unit> Handle(CreateThreadCommand request, CancellationToken cancellationToken)
    {
        var thread = Domain.Entities.Thread.Create(request.Title, request.CurrentUserId, request.BoardId);

        await threadRepository.AddAsync(thread, cancellationToken);
        await threadRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}