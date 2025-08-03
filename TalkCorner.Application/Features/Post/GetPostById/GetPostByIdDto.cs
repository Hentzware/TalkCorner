namespace TalkCorner.Application.Features.Post.GetPostById;

public class GetPostByIdDto
{
    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid CreatedByUserId { get; set; }

    public Guid Id { get; set; }

    public Guid ThreadId { get; set; }

    public Guid? ParentPostId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string CreatedByUsername { get; set; } = string.Empty;

    public string ThreadTitle { get; set; } = string.Empty;

    public string? ParentPostPreview { get; set; }
}