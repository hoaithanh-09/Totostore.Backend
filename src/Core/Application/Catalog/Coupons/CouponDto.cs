using Totostore.Backend.Application.Common.Enums;

namespace Totostore.Backend.Application.Catalog.Coupons;

public class CouponDto: IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CouponStatus Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TimeBegin { get; set; }
    public DateTime TimeEnd { get; set; }
    public int Quantity { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}