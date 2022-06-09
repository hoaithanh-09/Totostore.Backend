namespace Totostore.Backend.Application.Catalog.Carts;

public class CartDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { set; get; }
    public Guid CustomerId { get; set; }
    public int Quantity { set; get; }
    public decimal Price { set; get; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string ProductName { get; set; }
    public string CustomerName { get; set; }
}