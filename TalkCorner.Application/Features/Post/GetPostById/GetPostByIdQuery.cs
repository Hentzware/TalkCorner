using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Post.GetPostById;

public class GetPostByIdQuery : IRequest<GetPostByIdDto>
{
    [JsonIgnore]
    public Guid Id { get; set; }
};