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
    public SupplierDto Supplier { get; set; } = default!;
}