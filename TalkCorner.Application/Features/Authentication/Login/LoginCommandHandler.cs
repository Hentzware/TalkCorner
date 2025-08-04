using MediatR;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Features.Authentication.Common;

namespace TalkCorner.Application.Features.Authentication.Login;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, AuthenticationResponse>
{
    public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request);
        return response;
    }
}