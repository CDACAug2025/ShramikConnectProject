using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;

public class Client
{
    [Key]
    public int ClientId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Address { get; set; } = null!;
}
