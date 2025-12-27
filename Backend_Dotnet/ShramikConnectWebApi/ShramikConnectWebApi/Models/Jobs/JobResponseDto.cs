using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Models.Jobs;

public class JobResponseDto
{
    public int JobId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public SkillSet Category { get; set; }
    public string Location { get; set; } = null!;
    public decimal Budget { get; set; }
    public JobStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public int PostedByUserId { get; set; }
    public string PostedByName { get; set; } = null!;
}