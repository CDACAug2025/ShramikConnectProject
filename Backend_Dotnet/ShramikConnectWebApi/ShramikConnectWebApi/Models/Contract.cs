using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;
public class Contract
{
    [Key]
    public int ContractId { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; } = null!;

    public int WorkerUserId { get; set; }
    public User WorkerUser { get; set; } = null!;

    public int ClientUserId { get; set; }
    public User ClientUser { get; set; } = null!;

    public string ContractTerms { get; set; } = null!;
    public decimal AgreedAmount { get; set; }
    public ContractStatus Status { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? SignedAt { get; set; }

    public ICollection<EscrowPayment> EscrowPayments { get; set; } = new List<EscrowPayment>();
    public ChatRoom ChatRoom { get; set; } = null!;
    public ICollection<Dispute> Disputes { get; set; } = new List<Dispute>();
}
