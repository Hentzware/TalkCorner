using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.Thread.GetThreadsByBoardId;

public class GetThreadsByBoardIdQuery : IRequest<IEnumerable<GetThreadsByBoardIdDto>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}