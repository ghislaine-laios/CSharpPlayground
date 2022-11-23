using DemoSite.Models.Domain;
using DemoSite.Models.DTO;

namespace DemoSite.Ports
{
    public interface IPostRepository
    {
        Task<Post?> Import(long id);
        Task<IList<Post>> Import(TimeDescendingPostsQuery query);
        Task<long> Export(Post post);
        Task Update(Post post);
    }
}
