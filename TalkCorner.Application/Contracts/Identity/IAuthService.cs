using TalkCorner.Application.Features.Authentication.Common;
using TalkCorner.Application.Features.Authentication.Login;
using TalkCorner.Application.Features.Authentication.Register;

namespace TalkCorner.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthenticationResponse> LoginAsync(LoginCommand request);

    Task<AuthenticationResponse> RegisterAsync(RegisterCommand request);
}