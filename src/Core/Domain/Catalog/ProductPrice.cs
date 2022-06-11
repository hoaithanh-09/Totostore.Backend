namespace Totostore.Backend.Domain.Catalog;

public class ProductPrice : AuditableEntity, IAggregateRoot
{
    public ProductPrice(Guid productId, decimal amount)
    {
        ProductId = productId;
        Amount = amount;
    }

    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual List<OrderProduct> OrderProducts { get; set; } = default!;

    public ProductPrice Update(Guid? productId, decimal? amount)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (amount.HasValue && Amount != amount) Amount = amount.Value;
        return this;
    }
}