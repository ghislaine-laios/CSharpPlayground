using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DemoSite.Repositories;

public class CommentRepository : DatabaseRepositoryBase, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Comment?> Import(long id)
    {
        return await DbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IList<Comment>> Import(CommentsPerPostDescendingQuery query)
    {
        IQueryable<Comment> a = DbContext.Comments.OrderByDescending(c => c.CreatedTime);
        a = query.StartTime is not null ?
            a.Where(c => c.CreatedTime < query.StartTime && c.PostId == query.PostId) :
            a.Where(c => c.PostId == query.PostId);
        return await a.Take(query.Limit).ToListAsync();
    }

    public async Task<long> Export(Comment comment)
    {
        DbContext.Comments.Add(comment);
        await DbContext.SaveChangesAsync();
        return comment.Id;
    }
}