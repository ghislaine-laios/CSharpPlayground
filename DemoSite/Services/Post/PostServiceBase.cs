using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.Post
{
    public class PostServiceBase: DbContextServiceBase
    {
        protected readonly IPostRepository _postRepository;
        protected PostServiceBase(ApplicationDbContext dbContext, IPostRepository postRepository) : base(dbContext)
        {
            _postRepository = postRepository;
        }
    }
}
