namespace TalkCorner.Application.Features.Board.GetAllBoards;

public class GetAllBoardsDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ThreadCount { get; set; }

    public int SubBoardCount { get; set; }

    public Guid? ParentBoardId { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }
}