using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;

public class Organization
{
    [Key]
    public int OrgId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string OrgName { get; set; } = null!;
    public string GSTNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
}
