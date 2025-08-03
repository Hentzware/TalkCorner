using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.UpdateThread;

public class UpdateThreadCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
}