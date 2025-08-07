namespace TalkCorner.Application.Features.Thread.GetThreadById;

public class GetThreadByIdDto
{
    public bool IsClosed { get; set; }

    public bool IsSticky { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid BoardId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid Id { get; set; }

    public List<GetThreadByIdPostListItemDto> Posts { get; set; } = [];

    public string BoardTitle { get; set; } = string.Empty;

    public string CreatedByUsername { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}