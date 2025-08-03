using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalkCorner.Application.Features.Thread.CreateThread;
using TalkCorner.Application.Features.Thread.DeleteThread;
using TalkCorner.Application.Features.Thread.GetThreadById;
using TalkCorner.Application.Features.Thread.GetThreadsByBoardId;
using TalkCorner.Application.Features.Thread.UpdateThread;

namespace TalkCorner.API.Controllers;

[Route("api/threads")]
[ApiController]
[ProducesErrorResponseType(typeof(ProblemDetails))]
public class ThreadController(IMediator mediator) : ControllerBase
{
    [HttpGet("board/{boardId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<GetThreadsByBoardIdDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetThreadsByBoardIdDto>>> GetThreadsByBoardIdAsync(Guid boardId)
    {
        var request = new GetThreadsByBoardIdQuery(boardId);
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetThreadByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetThreadByIdDto>> GetThreadByIdAsync(Guid id)
    {
        var request = new GetThreadByIdQuery(id);
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateThreadAsync([FromBody] CreateThreadCommand request)
    {
        await mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created);
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

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteThreadAsync(Guid id)
    {
        var request = new DeleteThreadCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }
}