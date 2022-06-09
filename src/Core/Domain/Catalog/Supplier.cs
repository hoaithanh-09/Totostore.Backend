namespace Totostore.Backend.Domain.Catalog;

public class Supplier : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid AddressId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Address Address { get; set; } = default!;
    public virtual List<Product> Products { get; set; } = default!;
}