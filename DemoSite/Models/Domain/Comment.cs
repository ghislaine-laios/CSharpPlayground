namespace DemoSite.Models.Domain
{
    public class Comment
    {
        public long Id { get; set; }
        public required long SenderId { get; set; }
        public required long PostId { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedTime { get; set; }
    }
}
