using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.Post
{
    public interface ICreatePostService
    {
        Task<long> Execute(long userId, PostPayload payload);
    }

    public class CreatePostService: DbContextServiceBase, ICreatePostService
    {
        private readonly IPostRepository _postRepository;
        public CreatePostService(IPostRepository postRepository, ApplicationDbContext context) : base(context)
        {
            _postRepository = postRepository;
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
