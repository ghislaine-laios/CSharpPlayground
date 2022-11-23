using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoSite.Models.DTO;

public class CommentsPerPostDescendingQuery
{
    public required long PostId { get; set; }
    public DateTime? StartTime { get; set; }
    public int Limit { get; set; } = 100;
}