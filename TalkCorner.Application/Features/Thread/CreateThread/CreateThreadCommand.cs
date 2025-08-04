using System.Text.Json.Serialization;
using MediatR;
using TalkCorner.Application.Contracts.Common;

namespace TalkCorner.Application.Features.Thread.CreateThread;

public class CreateThreadCommand : IRequest<Unit>, IUserContextAware
{
    public Guid BoardId { get; set; }

    public string Title { get; set; } = string.Empty;

    [JsonIgnore]
    public Guid CurrentUserId { get; set; }
}