using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.GetThreadById;

public class GetThreadByIdQuery : IRequest<GetThreadByIdDto>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}