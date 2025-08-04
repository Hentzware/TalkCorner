using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalkCorner.Application.Features.Board.CreateBoard;
using TalkCorner.Application.Features.Board.DeleteBoard;
using TalkCorner.Application.Features.Board.GetAllBoards;
using TalkCorner.Application.Features.Board.GetBoardById;
using TalkCorner.Application.Features.Board.UpdateBoard;

namespace TalkCorner.API.Controllers;

[Route("api/boards")]
[ApiController]
public class BoardController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllBoardsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetAllBoardsDto>>> GetAllBoardsAsync()
    {
        var response = await mediator.Send(new GetAllBoardsQuery());
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetBoardByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetBoardByIdDto>> GetBoardByIdAsync(Guid id)
    {
        var response = await mediator.Send(new GetBoardByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateBoardAsync([FromBody] CreateBoardCommand request)
    {
        await mediator.Send(request);
        return Created();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteBoardAsync(Guid id)
    {
        await mediator.Send(new DeleteBoardCommand { Id = id });
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateBoardAsync(Guid id, [FromBody] UpdateBoardCommand request)
    {
        request.Id = id;
        await mediator.Send(request);
        return NoContent();
    }
}
