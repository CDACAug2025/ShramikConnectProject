using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;

public class Dispute
{
    [Key]
    public int DisputeId { get; set; }

    public int ContractId { get; set; }
    public Contract Contract { get; set; } = null!;

    public int RaisedByUserId { get; set; }
    public User RaisedByUser { get; set; } = null!;

    public string Reason { get; set; } = null!;
    public string Status { get; set; } = null!;

    public int? ResolvedByUserId { get; set; }
    public User? ResolvedByUser { get; set; }
}
