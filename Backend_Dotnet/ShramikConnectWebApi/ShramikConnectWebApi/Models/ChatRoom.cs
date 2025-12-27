using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;

public class ChatRoom
{
    [Key]
    public int ChatRoomId { get; set; }

    public int ContractId { get; set; }
    public Contract Contract { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}
