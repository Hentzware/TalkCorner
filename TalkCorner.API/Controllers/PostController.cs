using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalkCorner.API.Models;
using TalkCorner.Application.Features.Post.CreatePost;
using TalkCorner.Application.Features.Post.DeletePost;
using TalkCorner.Application.Features.Post.GetPostById;
using TalkCorner.Application.Features.Post.GetPostByThreadId;
using TalkCorner.Application.Features.Post.UpdatePost;

namespace TalkCorner.API.Controllers;

[Route("api/posts")]
[ApiController]
[ProducesErrorResponseType(typeof(CustomProblemDetails))]
public class PostController(IMediator mediator) : ControllerBase
{
    [HttpGet("thread/{threadId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<GetPostByThreadIdDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetPostByThreadIdDto>>> GetPostsByThreadIdAsync(Guid threadId)
    {
        var request = new GetPostByThreadIdQuery(threadId);
        var response = await mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetPostByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPostByIdDto>> GetPostByIdAsync(Guid id)
    {
        var request = new GetPostByIdQuery(id);
        var response = await mediator.Send(request);
        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostCommand request)
    {
        await mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePostAsync(Guid id, [FromBody] UpdatePostCommand request)
    {
        request.Id = id;
        await mediator.Send(request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePostAsync(Guid id)
    {
        var request = new DeletePostCommand { Id = id };
        await mediator.Send(request);
        return NoContent();
    }
}