using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalkCorner.Application.Features.Authentication;
using TalkCorner.Application.Features.Authentication.Login;
using TalkCorner.Application.Features.Authentication.Register;

namespace TalkCorner.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync([FromBody] LoginCommand request)
    {
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType<AuthenticationResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthenticationResponse>> RegisterAsync([FromBody] RegisterCommand request)
    {
        var response = await mediator.Send(request);
        return Ok(response);
    }
}