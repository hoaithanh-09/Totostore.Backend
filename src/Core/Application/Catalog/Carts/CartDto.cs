namespace Totostore.Backend.Application.Catalog.Carts;

public class CartDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Userid { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ProductName { get; set; } = default!;
}