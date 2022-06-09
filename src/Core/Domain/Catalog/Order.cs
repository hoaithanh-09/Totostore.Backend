namespace Totostore.Backend.Domain.Catalog;

public class Order : AuditableEntity, IAggregateRoot
{
    public decimal Amount { get; set; }
    public string Note { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public Guid ShipperId { get; set; }
    public Guid AddressDeliveryId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual List<OrderCoupon> OrderCoupons { get; set; } = default!;
    public virtual List<OrderPayment> OrderPayments { get; set; } = default!;
    public virtual List<OrderProduct> OrderProducts { get; set; } = default!;
    public virtual List<OrderStatus> OrderStatuses { get; set; } = default!;
}