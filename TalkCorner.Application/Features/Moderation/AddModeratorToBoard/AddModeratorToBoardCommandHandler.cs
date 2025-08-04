using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Moderation.AddModeratorToBoard;

public class AddModeratorToBoardCommandHandler(IBoardRepository boardRepository, IUserRepository userRepository) : IRequestHandler<AddModeratorToBoardCommand, Unit>
{
    public async Task<Unit> Handle(AddModeratorToBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await boardRepository.GetBoardByIdWithTrackingAsync(request.BoardId);

        if (board == null)
        {
            throw new InvalidOperationException("Board does not exist.");
        }

        var user = await userRepository.GetByIdWithTrackingAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            throw new InvalidOperationException("User does not exist.");
        }

        if (board.Moderators.Any(m => m.Id == user.Id))
        {
            throw new InvalidOperationException("User is already a moderator of this board.");
        }

        await boardRepository.UpdateAsync(board, cancellationToken);
        await boardRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}