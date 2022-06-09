using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class OrderStatus : AuditableEntity, IAggregateRoot
{
    public OrderStatusEnums Status { get; set; }
    public string Note { get; set; } = default!;
    public Guid OrderId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Order Order { get; set; } = default!;
}