using MediatR;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.Authentication.Register;

public class RegisterCommandHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<RegisterCommand, AuthenticationResponse>
{
    public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await authService.RegisterAsync(request);
        return response;
    }
}