using Totostore.Backend.Application.Catalog.Details;
using Totostore.Backend.Application.Catalog.Products;

namespace Totostore.Backend.Application.Catalog.ProductDetails;

public class OrderDetailDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid DetailId { get; set; }
    public ProductDto Product { get; set; } = default!;
    public DetailDto Detail { get; set; } = default!;
}