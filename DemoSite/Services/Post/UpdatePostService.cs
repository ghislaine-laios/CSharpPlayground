using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.Post
{
    public interface IUpdatePostService
    {
        Task Execute(long userId, PostPayloadWithId payload);
    }

    public class UpdatePostService : PostServiceBase, IUpdatePostService
    {
        public UpdatePostService(ApplicationDbContext dbContext, IPostRepository postRepository)
            : base(dbContext, postRepository)
        {
        }

        public async Task Execute(long userId, PostPayloadWithId payload)
        {
            await WithTransaction(async _ =>
            {
                var post = await PostRepository.Import(payload.Id);
                if (post is null) throw new PostConflictException();
                    if (post.UserId != userId) throw new PostNotBelongToThisUserException();
                post.Title = payload.Title;
                post.Content = payload.Content;
                post.LastUpdatedTime = DateTime.UtcNow;
                await PostRepository.Update(post);
                return "";
            });
        }
    }

    public class PostNotBelongToThisUserException : Exception
    {
        public PostNotBelongToThisUserException(string message = "This post doesn't belong to current user.") : base(message) { }
    }
}
