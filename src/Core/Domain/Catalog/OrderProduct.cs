namespace Totostore.Backend.Domain.Catalog;

public class OrderProduct : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductPriceId { get; set; }
    public int Quantity { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Product Product { get; set; } = default!;
    public virtual Order Order { get; set; } = default!;
    public virtual ProductPrice ProductPrice { get; set; } = default!;
}