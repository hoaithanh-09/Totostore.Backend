namespace Totostore.Backend.Application.Catalog.OrderProducts;

public class OrderProductDto :IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductPriceId { get; set; }
    public int Quantity { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string ProductName { get; set; } = default!;
    public string OrderName { get; set; } = default!;
}