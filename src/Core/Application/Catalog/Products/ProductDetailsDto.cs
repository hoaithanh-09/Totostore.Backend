using AutoMapper;
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
    public CategoryDto Category { get; set; }
    public Guid PriceId { get; set; }
    public ProductPriceDto ProductPrice { get; set; }
    public Guid? CouponId { get; set; }
    public Coupon Coupon { get; set; }

    public static ProductDetailsDto MapSomeThingCustom(IMapper mapper, Product src)
    {
        var mappedItem = mapper.Map<Product, ProductDetailsDto>(src);
        var categoryProduct = src.CategoryProducts.LastOrDefault();
        if (categoryProduct != null)
        {
            mappedItem.Category = mapper.Map<CategoryDto>(categoryProduct.Category);
            mappedItem.CategoryId = mappedItem.Category.Id;
        }
        var productPrice = src.ProductPrices.LastOrDefault();
        if(productPrice != null)
        {
            mappedItem.ProductPrice = mapper.Map<ProductPriceDto>(productPrice.Product);
            mappedItem.PriceId = mappedItem.ProductPrice.Id;
        }
        return mappedItem;
    }


}