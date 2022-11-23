using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.Post
{
    public class PostServiceBase: DbContextServiceBase
    {
        protected readonly IPostRepository PostRepository;
        protected PostServiceBase(ApplicationDbContext dbContext, IPostRepository postRepository) : base(dbContext)
        {
            PostRepository = postRepository;
        }
    }
}
