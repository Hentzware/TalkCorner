using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalkCorner.API.Models;
using TalkCorner.Application.Features.Thread.CloseThread;
using TalkCorner.Application.Features.Thread.CreateThread;
using TalkCorner.Application.Features.Thread.DeleteThread;
using TalkCorner.Application.Features.Thread.GetThreadById;
using TalkCorner.Application.Features.Thread.GetThreadsByBoardId;
using TalkCorner.Application.Features.Thread.OpenThread;
using TalkCorner.Application.Features.Thread.StickThread;
using TalkCorner.Application.Features.Thread.UnstickThread;
using TalkCorner.Application.Features.Thread.UpdateThread;

namespace TalkCorner.API.Controllers;

[Route("api/threads")]
[ApiController]
[ProducesErrorResponseType(typeof(CustomProblemDetails))]
public class ThreadController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetThreadByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetThreadByIdDto>> GetThreadByIdAsync(Guid id)
    {
        var request = new GetThreadByIdQuery { Id = id };
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("board/{boardId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<GetThreadsByBoardIdDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetThreadsByBoardIdDto>>> GetThreadsByBoardIdAsync(Guid boardId)
    {
        var request = new GetThreadsByBoardIdQuery { Id = boardId };
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("close/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CloseThreadAsync(Guid id)
    {
        var request = new CloseThreadCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateThreadAsync([FromBody] CreateThreadCommand request)
    {
        await mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteThreadAsync(Guid id)
    {
        var request = new DeleteThreadCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }

    [HttpPost("open/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> OpenThreadAsync(Guid id)
    {
        var request = new OpenThreadCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }

    [HttpPost("stick/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> StickThreadAsync(Guid id)
    {
        var request = new StickThreadCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }

    [HttpPost("unstick/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnstickThreadAsync(Guid id)
    {
        var request = new UnstickThreadCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateThreadAsync(Guid id, [FromBody] UpdateThreadCommand request)
    {
        request.Id = id;
        await mediator.Send(request);
        return NoContent();
    }
}