using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class OrderStatus : AuditableEntity, IAggregateRoot
{
    public OrderStatus(OrderStatusEnums status, string? note, Guid orderId)
    {
        Status = status;
        Note = note;
        OrderId = orderId;
    }

    public OrderStatusEnums Status { get; set; }
    public string? Note { get; set; } = default!;
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; } = default!;

    public OrderStatus Update(OrderStatusEnums? status, string? note, Guid? orderId)
    {
        if (status.HasValue && Status != status) Status = status.Value;
        if (note is not null && Note?.Equals(note) is not true) Note = note;
        if (orderId.HasValue && orderId.Value != Guid.Empty && !OrderId.Equals(orderId.Value)) OrderId = orderId.Value;
        return this;
    }
}