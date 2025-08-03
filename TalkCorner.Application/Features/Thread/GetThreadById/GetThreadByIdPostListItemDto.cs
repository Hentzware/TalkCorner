namespace TalkCorner.Application.Features.Thread.GetThreadById;

public class GetThreadByIdPostListItemDto
{
    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public string CreatedByUsername { get; set; } = string.Empty;
}