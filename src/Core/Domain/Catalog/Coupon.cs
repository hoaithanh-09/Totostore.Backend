using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class Coupon : AuditableEntity, IAggregateRoot
{
    public Coupon(string name, CouponStatus type, decimal amount, DateTime timeBegin, DateTime timeEnd, int quantity)
    {
        Name = name;
        Type = type;
        Amount = amount;
        TimeBegin = timeBegin;
        TimeEnd = timeEnd;
        Quantity = quantity;
    }

    public string Name { get; set; }
    public CouponStatus Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TimeBegin { get; set; }
    public DateTime TimeEnd { get; set; }
    public int Quantity { get; set; }
    public virtual List<OrderCoupon> OrderCoupons { get; set; } = default!;
    public virtual List<Order> Orders { get; set; } = default!;

    public Coupon Update(string? name, CouponStatus? type, decimal? amount, DateTime? timeBegin, DateTime? timeEnd,
        int? quantity)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (type.HasValue && Type != type) Type = type.Value;
        if (amount.HasValue && Amount != amount) Amount = amount.Value;
        if (timeBegin.HasValue && TimeBegin != timeBegin) TimeBegin = timeBegin.Value;
        if (timeEnd.HasValue && TimeEnd != timeEnd) TimeEnd = timeEnd.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        return this;
    }
}