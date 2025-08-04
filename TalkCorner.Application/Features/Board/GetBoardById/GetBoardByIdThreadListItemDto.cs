namespace TalkCorner.Application.Features.Board.GetBoardById;

public class GetBoardByIdThreadListItemDto
{
    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid Id { get; set; }

    public int PostCount { get; set; }

    public string CreatedByUsername { get; set; } = null!;

    public string Title { get; set; } = null!;
}