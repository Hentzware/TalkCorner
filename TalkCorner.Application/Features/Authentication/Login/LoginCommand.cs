using MediatR;
using TalkCorner.Application.Features.Authentication.Common;

namespace TalkCorner.Application.Features.Authentication.Login;

public class LoginCommand : IRequest<AuthenticationResponse>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}