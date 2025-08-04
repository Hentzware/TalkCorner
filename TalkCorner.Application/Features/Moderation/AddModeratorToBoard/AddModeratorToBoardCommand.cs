using MediatR;
using System;

namespace TalkCorner.Application.Features.Moderation.AddModeratorToBoard;

public class AddModeratorToBoardCommand : IRequest<Unit>
{
    public Guid BoardId { get; set; }
    public Guid UserId { get; set; }
}