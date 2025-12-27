using System.ComponentModel.DataAnnotations;

namespace ShramikConnectWebApi.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
