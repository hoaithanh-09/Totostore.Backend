using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class OrderPayment : AuditableEntity, IAggregateRoot
{
    public OrderPayment(Guid orderId, Guid paymentId, PaymentStatus status, decimal amount)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        Status = status;
        Amount = amount;
    }

    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public virtual Order Order { get; set; } = default!;
    public virtual Payment Payment { get; set; } = default!;

    public OrderPayment Update(Guid? orderId, Guid? paymentId, PaymentStatus? status, decimal? amount)
    {
        if (orderId.HasValue && orderId.Value != Guid.Empty && !OrderId.Equals(orderId.Value)) OrderId = orderId.Value;
        if (paymentId.HasValue && paymentId.Value != Guid.Empty && !PaymentId.Equals(paymentId.Value))
            PaymentId = paymentId.Value;
        if (status.HasValue && Status != status) Status = status.Value;
        if (amount.HasValue && Amount != amount) Amount = amount.Value;
        return this;
    }
}