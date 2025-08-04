using System.Text.Json.Serialization;
using MediatR;
using TalkCorner.Application.Contracts.Common;

namespace TalkCorner.Application.Features.Board.CreateBoard;

public class CreateBoardCommand : IRequest<Unit>, IUserContextAware
{
    public Guid? ParentBoardId { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    [JsonIgnore]
    public Guid CurrentUserId { get; set; }
}