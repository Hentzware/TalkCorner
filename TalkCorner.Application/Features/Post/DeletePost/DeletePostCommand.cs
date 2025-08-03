using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Post.DeletePost;

public class DeletePostCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}