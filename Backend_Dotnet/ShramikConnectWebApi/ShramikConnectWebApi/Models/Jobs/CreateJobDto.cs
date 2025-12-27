using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Models.Jobs;

public class CreateJobDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public SkillSet Category { get; set; }
    public string Location { get; set; } = null!;
    public decimal Budget { get; set; }
}