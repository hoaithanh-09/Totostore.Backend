namespace Totostore.Backend.Application.Catalog.Coupons;

public class CouponByNameSpec : Specification<Coupon>, ISingleResultSpecification
{
    public CouponByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}