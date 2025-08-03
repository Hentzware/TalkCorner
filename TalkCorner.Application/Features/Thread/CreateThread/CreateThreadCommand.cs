using MediatR;

namespace TalkCorner.Application.Features.Thread.CreateThread;

public class CreateThreadCommand : IRequest<Unit>
{
    public Guid BoardId { get; set; }

    public Guid CreatedByUserId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}