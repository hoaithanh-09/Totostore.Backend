namespace Totostore.Backend.Domain.Catalog;

public class Payment : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; } = default!;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual List<OrderPayment> OrderPayments { get; set; } = default!;
}