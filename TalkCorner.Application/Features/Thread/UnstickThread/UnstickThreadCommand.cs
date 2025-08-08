using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.UnstickThread;

public class UnstickThreadCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}