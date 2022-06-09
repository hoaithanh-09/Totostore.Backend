namespace Totostore.Backend.Application.Catalog.OrderCoupons;

public class OrderCouponDto: IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid CouponId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string OrderName { get; set; }
    public string CouponName { get; set; }
}