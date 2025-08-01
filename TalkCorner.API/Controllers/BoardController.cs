using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalkCorner.Application.Features.Board.GetAllBoardsQuery;

namespace TalkCorner.API.Controllers;

[Route("api/boards")]
[ApiController]
public class BoardController(IMediator mediator) : ControllerBase
{
    public async Task<ActionResult<IEnumerable<GetAllBoardsDto>>> GetAsync()
    {
        var request = new GetAllBoardsQuery();
        var response = await mediator.Send(request);
        return Ok(response);
    }
}