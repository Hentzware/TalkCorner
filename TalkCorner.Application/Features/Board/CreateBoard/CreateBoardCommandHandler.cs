using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Board.CreateBoard;

public class CreateBoardCommandHandler(IBoardRepository boardRepository, IMapper mapper) : IRequestHandler<CreateBoardCommand, Unit>
{
    public async Task<Unit> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = mapper.Map<Domain.Entities.Board>(request);

        await boardRepository.AddAsync(board, cancellationToken);
        await boardRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}