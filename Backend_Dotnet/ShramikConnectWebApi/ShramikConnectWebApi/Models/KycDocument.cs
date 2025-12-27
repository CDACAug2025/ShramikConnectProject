using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;
public class KycDocument
{
    [Key]
    public int KycId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DocumentType DocumentType { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public KycStatus Status { get; set; }

    public int? VerifiedByUserId { get; set; }
    public User? VerifiedByUser { get; set; }

    public DateTime? VerifiedAt { get; set; }
}
