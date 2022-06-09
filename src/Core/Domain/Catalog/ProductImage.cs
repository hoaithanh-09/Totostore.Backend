using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class ProductImage : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; set; }
    public ProductImageType Type { get; set; }
    public string? Description { get; set; }
    public virtual Product Product { get; set; } = default!;
}