using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.Post
{
    public interface ICreatePostService
    {
        Task<long> Execute(long userId, PostPayload payload);
    }

    public class CreatePostService: PostServiceBase, ICreatePostService
    {
        public CreatePostService(ApplicationDbContext dbContext, IPostRepository postRepository)
            : base(dbContext, postRepository)
        {
        }

        public async Task<long> Execute(long userId, PostPayload payload)
        {
            return await WithTransaction(async _ =>
            {
                var newPost = payload.ToPost(userId);
                return await _postRepository.Export(newPost);
            });
        }
    }
}
