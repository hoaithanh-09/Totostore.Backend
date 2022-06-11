namespace Totostore.Backend.Domain.Catalog;

public class OrderCoupon : AuditableEntity, IAggregateRoot
{
    public OrderCoupon(Guid orderId, Guid couponId)
    {
        OrderId = orderId;
        CouponId = couponId;
    }

    public Guid OrderId { get; set; }
    public Guid CouponId { get; set; }
    public virtual Order Order { get; set; } = default!;
    public virtual Coupon Coupon { get; set; } = default!;

    public OrderCoupon Update(Guid? orderId, Guid? couponId)
    {
        if (orderId.HasValue && orderId.Value != Guid.Empty && !OrderId.Equals(orderId.Value)) OrderId = orderId.Value;
        if (couponId.HasValue && couponId.Value != Guid.Empty && !CouponId.Equals(couponId.Value))
            CouponId = couponId.Value;
        return this;
    }
}