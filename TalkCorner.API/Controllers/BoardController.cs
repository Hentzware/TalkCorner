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
    public async Task<ActionResult<IEnumerable<GetAllBoardsDto>>> GetAllBoardsAsync()
    {
        var request = new GetAllBoardsQuery();
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetBoardByIdDto>> GetBoardByIdAsync(Guid id)
    {
        var request = new GetBoardByIdQuery(id);
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateBoardAsync([FromBody] CreateBoardCommand request)
    {
        await mediator.Send(request);
        return Created();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteBoardAsync(Guid id)
    {
        var request = new DeleteBoardCommand() { Id = id };
        await mediator.Send(request);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateBoardAsync(Guid id, [FromBody] UpdateBoardCommand request)
    {
        request.Id = id;
        await mediator.Send(request);
        return NoContent();
    }
}