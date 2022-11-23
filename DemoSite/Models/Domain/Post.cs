using Microsoft.EntityFrameworkCore;

namespace DemoSite.Models.Domain
{
    public class PostBase
    { 
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    [Index(nameof(Title))]
    [Index(nameof(UserId), nameof(Title), IsUnique = true)]
    [Index(nameof(CreatedTime))]
    public class Post: PostBase
    {
        public long Id { get; set; }
        public required long UserId { get; set; }
        public required DateTime CreatedTime { get; set; }
        public required DateTime LastUpdatedTime { get; set; }
    }

    public class PostConflictException : Exception
    {
        public PostConflictException(string message = "The user already has a post with the same title."): base(message) { }
    }
}
