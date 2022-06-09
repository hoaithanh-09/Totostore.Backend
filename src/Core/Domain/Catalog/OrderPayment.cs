using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class OrderPayment : AuditableEntity, IAggregateRoot
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Order Order { get; set; } = default!;
    public virtual Payment Payment { get; set; } = default!;
}