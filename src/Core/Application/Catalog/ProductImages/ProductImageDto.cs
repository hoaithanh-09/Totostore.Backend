using Totostore.Backend.Application.Common.Enums;
namespace Totostore.Backend.Application.Catalog.ProductImages;

public class ProductImageDto:IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductImageType Type { get; set; }
    public string? Description { get; set; }
}