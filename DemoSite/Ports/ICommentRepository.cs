using DemoSite.Models.Domain;
using DemoSite.Models.DTO;

namespace DemoSite.Ports;

public interface ICommentRepository
{
    Task<Comment?> Import(long id);
    Task<IList<Comment>> Import(CommentsPerPostDescendingQuery query);
    Task<long> Export(Comment comment);
}