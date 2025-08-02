namespace TalkCorner.Application.Features.Board.GetBoardById;

public class GetBoardByIdBoardListItemDto
{
    public Guid Id { get; set; }

    public int ThreadCount { get; set; }

    public string Title { get; set; } = null!;
}