using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.OpenThread;

public class OpenThreadCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}