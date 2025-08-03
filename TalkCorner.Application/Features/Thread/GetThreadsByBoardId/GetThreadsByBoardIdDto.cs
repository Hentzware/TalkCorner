namespace TalkCorner.Application.Features.Thread.GetThreadsByBoardId;

public class GetThreadsByBoardIdDto
{
    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid Id { get; set; }

    public int PostCount { get; set; }

    public string CreatedByUsername { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}