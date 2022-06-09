namespace Totostore.Backend.Domain.Catalog;

public class ProductPrice : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Product Product { get; set; } = default!;
    public virtual List<OrderProduct> OrderProducts { get; set; } = default!;
}