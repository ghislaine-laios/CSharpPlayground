using DemoSite.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Models.DTO;

public class CommentPayload
{
    public required long PostId { get; set; }
    public required string Content { get; set; }

    public Comment ToComment(long userId)
    {
        return new Comment { PostId = PostId, Content = Content, CreatedTime = DateTime.UtcNow, SenderId = userId};
    }
}