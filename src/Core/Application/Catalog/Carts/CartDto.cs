namespace Totostore.Backend.Application.Catalog.Carts;

public class CartDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string ProductName { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
}