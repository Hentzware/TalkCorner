using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Board.CreateBoard;

public class CreateBoardCommandHandler(IBoardRepository boardRepository) : IRequestHandler<CreateBoardCommand, Unit>
{
    public async Task<Unit> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = Domain.Entities.Board.Create(
            request.Title, 
            request.Description, 
            request.SortOrder, 
            request.CurrentUserId, 
            request.ParentBoardId);

        await boardRepository.AddAsync(board, cancellationToken);
        await boardRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}