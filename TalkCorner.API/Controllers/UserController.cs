using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalkCorner.API.Models;
using TalkCorner.Application.Features.User.DeleteUser;
using TalkCorner.Application.Features.User.GetAllUsers;
using TalkCorner.Application.Features.User.GetUserById;
using TalkCorner.Application.Features.User.UpdateUser;

namespace TalkCorner.API.Controllers;

[Route("api/users")]
[ApiController]
[ProducesErrorResponseType(typeof(CustomValidationProblemDetails))]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllUsersDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetAllUsersDto>>> GetAllUsersAsync()
    {
        var response = await mediator.Send(new GetAllUsersQuery());
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetUserByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserByIdDto>> GetUserByIdAsync(Guid id)
    {
        var response = await mediator.Send(new GetUserByIdQuery(id));
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UpdateUserCommand request)
    {
        request.Id = id;
        await mediator.Send(request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var request = new DeleteUserCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }
}