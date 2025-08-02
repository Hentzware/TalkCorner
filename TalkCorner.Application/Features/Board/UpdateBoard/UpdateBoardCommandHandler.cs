using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Board.UpdateBoard;

public class UpdateBoardCommandHandler(IBoardRepository boardRepository, IMapper mapper) : IRequestHandler<UpdateBoardCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await boardRepository.GetBoardByIdWithTrackingAsync(request.Id);

        if (board == null)
        {
            throw new Exception();
        }

        board.UpdateTitle(request.Title);
        board.UpdateDescription(request.Description);

        await boardRepository.UpdateAsync(board, cancellationToken);
        await boardRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}