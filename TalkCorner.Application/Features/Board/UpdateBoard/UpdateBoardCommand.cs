using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Board.UpdateBoard;

public class UpdateBoardCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public int SortOrder { get; set; }
}