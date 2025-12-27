using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;
public class Job
{
    [Key]
    public int JobId { get; set; }

    public int PostedByUserId { get; set; }
    public User PostedByUser { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public SkillSet Category { get; set; }
    public string Location { get; set; } = null!;
    public decimal Budget { get; set; }
    public JobStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    public Contract? Contract { get; set; }
}
