namespace Totostore.Backend.Application.Catalog.OrderCoupons;

public class OrderCouponDto : IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid CouponId { get; set; }
    public string OrderName { get; set; } = default!;
    public string CouponName { get; set; } = default!;
}