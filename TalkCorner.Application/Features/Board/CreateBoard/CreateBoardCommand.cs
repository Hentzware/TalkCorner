using MediatR;

namespace TalkCorner.Application.Features.Board.CreateBoard;

public class CreateBoardCommand : IRequest<Unit>
{
    public Guid CreatedByUserId { get; set; }

    public Guid? ParentBoardId { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}