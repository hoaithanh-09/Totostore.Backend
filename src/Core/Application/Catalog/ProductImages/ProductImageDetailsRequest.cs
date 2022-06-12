using Totostore.Backend.Application.Catalog.Products;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.ProductImages;

public class ProductImageDetailsRequest : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImagePath { get; set; } = default!;
    public long? FileSize { get; set; }
    public ProductImageType Type { get; set; }
    public string? Description { get; set; }
    public ProductDto Product { get; set; } = default!;
}