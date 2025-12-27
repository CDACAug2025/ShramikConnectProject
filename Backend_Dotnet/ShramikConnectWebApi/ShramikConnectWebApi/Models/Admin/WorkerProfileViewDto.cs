using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Models.Admin;

public class WorkerProfileViewDto
{
    public SkillSet SkillSet { get; set; }
    public int ExperienceYears { get; set; }
    public string Location { get; set; } = null!;
    public decimal Rating { get; set; }
}