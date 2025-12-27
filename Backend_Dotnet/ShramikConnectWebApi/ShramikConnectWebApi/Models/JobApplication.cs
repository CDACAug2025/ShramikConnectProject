using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;
public class JobApplication
{
    [Key]
    public int ApplicationId { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; } = null!;

    public int ApplicantUserId { get; set; }
    public User ApplicantUser { get; set; } = null!;

    public DateTime AppliedAt { get; set; }
    public ApplicationStatus Status { get; set; }
}
