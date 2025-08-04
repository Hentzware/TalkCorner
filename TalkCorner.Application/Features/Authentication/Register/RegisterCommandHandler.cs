using MediatR;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Features.Authentication.Common;

namespace TalkCorner.Application.Features.Authentication.Register;

public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, AuthenticationResponse>
{
    public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await authService.RegisterAsync(request);
        return response;
    }
}