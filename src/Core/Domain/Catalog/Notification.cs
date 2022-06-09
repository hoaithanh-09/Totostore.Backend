namespace Totostore.Backend.Domain.Catalog;

public class Notification : AuditableEntity, IAggregateRoot
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Guid? CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Customer Customer { get; set; } = default!;
    public virtual Shipper Shipper { get; set; } = default!;
}