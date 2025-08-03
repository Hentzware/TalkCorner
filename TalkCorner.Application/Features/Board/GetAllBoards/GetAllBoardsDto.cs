namespace TalkCorner.Application.Features.Board.GetAllBoards;

public class GetAllBoardsDto
{
    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid Id { get; set; }

    public Guid? ParentBoardId { get; set; }

    public int SubBoardCount { get; set; }

    public int ThreadCount { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;
}