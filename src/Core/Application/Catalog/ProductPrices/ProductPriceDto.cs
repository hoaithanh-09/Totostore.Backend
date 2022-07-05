namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class ProductPriceDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid? CouponId { get; set; }
    public decimal Amount { get; set; }
    public string ProductName { get; set; } = default!;
    public string? CouponName { get; set; } = default!;
}