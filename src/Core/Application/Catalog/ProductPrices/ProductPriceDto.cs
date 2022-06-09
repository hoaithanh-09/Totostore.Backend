namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class ProductPriceDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string ProductName { get; set; } = default!;
}