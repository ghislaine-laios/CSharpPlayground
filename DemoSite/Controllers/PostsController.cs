using DemoSite.Exceptions;
using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Services.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostsController(IPostRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<object> GetPosts([FromQuery] TimeDescendingPostsQuery query)
        {
            return await _repository.Import(query);
        }

        [HttpGet("{postId}")]
        public async Task<object> GetPost(long postId)
        {
            return await _repository.Import(postId);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<object> CreatePost([FromBody] PostPayload payload, ICreatePostService createPostService)
        {
            try
            {
                var userId = this.GetUserId();
                var postId = await createPostService.Execute(userId, payload);
                var result = new { postId };
                return CreatedAtAction(nameof(GetPost), result, result);
            }
            catch (PostConflictException e)
            {
                return this.Error(new HttpResponseException(e.Message, StatusCodes.Status409Conflict));
            }
        }

        [Authorize]
        [HttpPut("{postId}")]
        public async Task<object> UpdatePost([FromBody] PostPayload payload, long postId, IUpdatePostService updatePostService)
        {
            try
            {
                var userId = this.GetUserId();
                await updatePostService.Execute(userId,
                    new PostPayloadWithId { Id = postId, Title = payload.Title, Content = payload.Content });
                return NoContent();
            }
            catch (PostNotBelongToThisUserException e)
            {
                return this.Error(new HttpResponseException(e.Message, StatusCodes.Status403Forbidden));
            }
        }
    }
}
