using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Board.DeleteBoard;

public class DeleteBoardCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}