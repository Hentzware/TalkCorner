using MediatR;

namespace TalkCorner.Application.Features.Post.CreatePost;

public class CreatePostCommand : IRequest<Unit>
{
    public Guid CreatedByUserId { get; set; }

    public Guid ThreadId { get; set; }

    public Guid? ParentPostId { get; set; }

    public string Content { get; set; } = string.Empty;
}