namespace DemoSite.Models.DTO;

public class TimeDescendingPostsQuery
{
    public DateTime? StartTime { get; init; }
    public int Limit { get; init; } = 100;
}
