using DemoSite.Migrations;
using DemoSite.Models.Domain;
using DemoSite.Models.DTO;
using DemoSite.Ports;
using DemoSite.Repositories;

namespace DemoSite.Services.Comment;

public interface ICreateCommentService
{
    Task<long> Execute(long userId, CommentPayload payload);
}

public class CreateCommentService: CommentServiceBase, ICreateCommentService
{
    private readonly IPostRepository _postRepository;
    public CreateCommentService(
        ApplicationDbContext dbContext, ICommentRepository commentRepository, IPostRepository postRepository) 
        : base(dbContext, commentRepository)
    {
        this._postRepository = postRepository;
    }

    public async Task<long> Execute(long userId, CommentPayload payload)
    {
        return await WithTransaction(async _ =>
        {
            var post = await _postRepository.Import(payload.PostId);
            if (post is null) throw new PostNotFoundException();
            var comment = payload.ToComment(userId);
            return await CommentRepository.Export(comment);
        });
    }
}