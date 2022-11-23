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
}
