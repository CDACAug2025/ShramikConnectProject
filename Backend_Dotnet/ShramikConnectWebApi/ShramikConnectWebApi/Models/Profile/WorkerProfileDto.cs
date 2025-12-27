using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Models.Profile;

public class WorkerProfileDto
{
    public SkillSet SkillSet { get; set; }
    public int ExperienceYears { get; set; }
    public string Location { get; set; } = null!;
}