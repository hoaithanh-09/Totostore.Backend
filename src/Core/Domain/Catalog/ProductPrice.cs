using System.ComponentModel.DataAnnotations.Schema;

namespace Totostore.Backend.Domain.Catalog;

public class ProductPrice : AuditableEntity, IAggregateRoot
{
    public ProductPrice(Guid productId, Guid? couponId, decimal amount)
    {
        ProductId = productId;
        Amount = amount;
        CouponId = couponId;
    }

    public Guid ProductId { get; set; }

    [ForeignKey("CouponId")]
    public Guid? CouponId { get; set; }
    public decimal Amount { get; set; }
    public virtual Product Product { get; set; } 
    public Coupon? Coupon { get; set; }


    public ProductPrice Update(Guid? productId, Guid? couponId, decimal? amount)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (couponId.HasValue && couponId.Value != Guid.Empty && !CouponId.Equals(couponId.Value))
            CouponId = couponId.Value;
        if (amount.HasValue && Amount != amount) Amount = amount.Value;
        return this;
    }
}