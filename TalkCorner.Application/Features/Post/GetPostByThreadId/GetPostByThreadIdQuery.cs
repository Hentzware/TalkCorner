using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Post.GetPostByThreadId;

public class GetPostByThreadIdQuery : IRequest<IEnumerable<GetPostByThreadIdDto>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}