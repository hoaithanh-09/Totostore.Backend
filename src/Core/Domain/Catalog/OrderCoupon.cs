namespace Totostore.Backend.Domain.Catalog;

public class OrderCoupon : AuditableEntity, IAggregateRoot
{
    public Guid OrderId { get; set; }
    public Guid CouponId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Order Order { get; set; } = default!;
    public virtual Coupon Coupon { get; set; } = default!;
}