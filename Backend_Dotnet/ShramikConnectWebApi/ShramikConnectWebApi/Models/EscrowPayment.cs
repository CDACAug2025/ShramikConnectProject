using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;
public class EscrowPayment
{
    [Key]
    public int EscrowId { get; set; }

    public int ContractId { get; set; }
    public Contract Contract { get; set; } = null!;

    public decimal Amount { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
}
