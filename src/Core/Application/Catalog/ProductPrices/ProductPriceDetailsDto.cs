using Totostore.Backend.Application.Catalog.Coupons;
using Totostore.Backend.Application.Catalog.Products;

namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class ProductPriceDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid? CouponId { get; set; }
    public decimal Amount { get; set; }
    public ProductDto Product { get; set; } = default!;
    public CouponDto? Coupon { get; set; }
}