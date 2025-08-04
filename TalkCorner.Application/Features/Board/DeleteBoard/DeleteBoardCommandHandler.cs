using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.Board.DeleteBoard;

public class DeleteBoardCommandHandler(IBoardRepository boardRepository, IMapper mapper) : IRequestHandler<DeleteBoardCommand, Unit>
{
    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await boardRepository.GetBoardByIdWithTrackingAsync(request.Id);

        if (board == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Board), request.Id);
        }

        await boardRepository.DeleteAsync(board, cancellationToken);
        await boardRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}