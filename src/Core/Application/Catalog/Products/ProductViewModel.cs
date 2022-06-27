using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Products;

public class ProductViewModel : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public Status Status { get; set; }
    public Guid SupplierId { get; set; }
    public virtual Supplier Supplier { get; private set; } = default!;
    public Guid DetailId { get; set; }
    public virtual Detail Detail { get; private set; } = default!;
    public List<ProductImage> ProductImages { get; set; } = default!;
    public ProductPrice Price { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; private set; } = default!;
}