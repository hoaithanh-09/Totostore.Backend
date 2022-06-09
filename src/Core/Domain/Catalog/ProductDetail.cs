namespace Totostore.Backend.Domain.Catalog;

public class ProductDetail : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; set; }
    public Guid DetailId { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual Detail Detail { get; set; } = default!;
}