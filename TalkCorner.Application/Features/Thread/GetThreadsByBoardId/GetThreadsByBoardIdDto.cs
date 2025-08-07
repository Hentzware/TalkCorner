namespace TalkCorner.Application.Features.Thread.GetThreadsByBoardId;

public class GetThreadsByBoardIdDto
{
    public bool IsClosed { get; set; }

    public bool IsSticky { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid Id { get; set; }

    public int PostCount { get; set; }

    public string CreatedByUsername { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}