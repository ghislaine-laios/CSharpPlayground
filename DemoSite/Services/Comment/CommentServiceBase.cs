using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.Comment
{
    public class CommentServiceBase: DbContextServiceBase
    {
        protected readonly ICommentRepository CommentRepository;

        protected CommentServiceBase(ApplicationDbContext dbContext, ICommentRepository commentRepository) : base(dbContext)
        {
            CommentRepository = commentRepository;
        }
    }
}
