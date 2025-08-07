using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.CloseThread;

public class CloseThreadCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}