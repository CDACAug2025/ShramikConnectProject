using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;

public class ChatMessage
{
    [Key]
    public int MessageId { get; set; }

    public int ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; } = null!;

    public int SenderUserId { get; set; }
    public User SenderUser { get; set; } = null!;

    public bool IsAiResponse { get; set; }
    public string MessageText { get; set; } = null!;
    public DateTime SentAt { get; set; }
}
