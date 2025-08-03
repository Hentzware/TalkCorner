using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.DeleteThread;

public class DeleteThreadCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}