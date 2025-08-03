using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.User.DeleteUser;

public class DeleteUserCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}