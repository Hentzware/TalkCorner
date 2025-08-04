using System.Text.Json.Serialization;
using MediatR;
using TalkCorner.Application.Contracts.Common;

namespace TalkCorner.Application.Features.Post.CreatePost;

public class CreatePostCommand : IRequest<Unit>, IUserContextAware
{
    public Guid ThreadId { get; set; }

    public Guid? ParentPostId { get; set; }

    public string Content { get; set; } = string.Empty;

    [JsonIgnore]
    public Guid CurrentUserId { get; set; }
}