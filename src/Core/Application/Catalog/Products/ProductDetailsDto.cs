using Totostore.Backend.Application.Catalog.Categories;
using Totostore.Backend.Application.Catalog.CategoryProducts;
using Totostore.Backend.Application.Catalog.Coupons;
using Totostore.Backend.Application.Catalog.ProductPrices;
using Totostore.Backend.Application.Catalog.Suppliers;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Products;

public class ProductDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public Status Status { get; set; }
    public Guid SupplierId { get; set; }
    public SupplierDetailsDto Supplier { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Guid PriceId { get; set; }
    public ProductPrice ProductPrice { get; set; }
    public Guid? CouponId { get; set; }
    public Coupon Coupon { get; set; }


}