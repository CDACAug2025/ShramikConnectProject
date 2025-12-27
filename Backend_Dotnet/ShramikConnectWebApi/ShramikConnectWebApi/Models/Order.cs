using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;
public class Order
{
    [Key]
    public int OrderId { get; set; }

    public int BuyerUserId { get; set; }
    public User BuyerUser { get; set; } = null!;

    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    
    public OrderStatus Status { get; set; }


    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
