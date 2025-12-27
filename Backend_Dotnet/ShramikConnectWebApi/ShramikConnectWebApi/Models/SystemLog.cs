using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;

public class SystemLog
{
    [Key]
    public int LogId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Action { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
