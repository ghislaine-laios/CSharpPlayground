using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Services.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{

    [HttpGet]
    public async Task<object> GetCommentsOfAPost(
        [FromQuery] CommentsPerPostDescendingQuery descendingQuery, ICommentRepository repository)
    {
        return await repository.Import(descendingQuery);
    }

    [HttpGet("{id}")]
    public async Task<object> GetComment(long id, ICommentRepository repository)
    {
        var result = await repository.Import(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<object> CreateComment(CommentPayload payload, ICreateCommentService service)
    {
        try
        {
            var userId = this.GetUserId();
            var id = await service.Execute(userId, payload);
            var result = new { id };
            return CreatedAtAction(nameof(GetComment), result, result);
        }
        catch (PostNotFoundException)
        {
            return NotFound();
        }
    }
}