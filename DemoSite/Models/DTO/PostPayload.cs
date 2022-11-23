using DemoSite.Models.Domain;

namespace DemoSite.Models.DTO
{
    public class PostPayload : PostBase
    {
        public Post ToPost(long userId)
        {
            var now = DateTime.UtcNow;
            return new Post { UserId = userId, Title = Title, Content = Content, CreatedTime = now, LastUpdatedTime = now };
        }
    }

    public class PostPayloadWithId : PostBase
    {
        public long Id { get; set; }

        public Post ToPost(long userId, DateTime createdTime)
        {
            return new Post
            {
                Id = Id,
                UserId = userId,
                Title = Title,
                Content = Content,
                CreatedTime = createdTime,
                LastUpdatedTime = DateTime.UtcNow
            };
        }
    }
}
