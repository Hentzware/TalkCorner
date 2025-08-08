using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.StickThread;

public class StickThreadCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}