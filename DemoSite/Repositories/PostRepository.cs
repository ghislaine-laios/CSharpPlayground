using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using Microsoft.EntityFrameworkCore;

namespace DemoSite.Repositories
{
    public class PostRepository : DatabaseRepositoryBase, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Post> Import(long id)
        {
            return await DbContext.Posts.SingleAsync(p => p.Id == id);
        }

        public async Task<IList<Post>> Import(TimeDescendingPostsQuery query)
        {
            IQueryable<Post> a = DbContext.Posts.OrderByDescending(p => p.Id);
            if (query.StartTime is not null)
            {
                a = a.Where(p => p.CreatedTime < query.StartTime);
            }
            return await a.Take(query.Limit).ToListAsync();
        }

        public async Task<long> Export(Post post)
        {
            var anyPost = await DbContext.Posts.AnyAsync(p => p.UserId == post.UserId && p.Title == post.Title);
            if (anyPost) throw new PostConflictException();
            DbContext.Posts.Add(post);
            await DbContext.SaveChangesAsync();
            return post.Id;
        }
    }
}
