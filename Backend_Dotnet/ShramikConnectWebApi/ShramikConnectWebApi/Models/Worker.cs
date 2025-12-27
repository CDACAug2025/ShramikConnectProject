using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;

public class Worker
{
    [Key]
    public int WorkerId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public SkillSet SkillSet { get; set; }
    public int ExperienceYears { get; set; }
    public string Location { get; set; } = null!;
    public decimal Rating { get; set; }
}
