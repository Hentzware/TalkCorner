using System.Text.Json.Serialization;
using MediatR;

namespace TalkCorner.Application.Features.User.UpdateUser;

public class UpdateUserCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;
}