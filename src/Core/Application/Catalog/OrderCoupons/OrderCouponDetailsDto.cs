using Totostore.Backend.Application.Catalog.Coupons;
using Totostore.Backend.Application.Catalog.Orders;

namespace Totostore.Backend.Application.Catalog.OrderCoupons;

public class OrderCouponDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid CouponId { get; set; }
    public OrderDto Order { get; set; } = default!;
    public CouponDto Coupon { get; set; } = default!;
}