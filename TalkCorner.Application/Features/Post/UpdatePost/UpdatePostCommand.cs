using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Post.UpdatePost;

public class UpdatePostCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;
}