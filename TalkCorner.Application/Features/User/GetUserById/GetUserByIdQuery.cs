using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.User.GetUserById;

public class GetUserByIdQuery : IRequest<GetUserByIdDto>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}