namespace Totostore.Backend.Domain.Catalog;

public class ProductDetail : AuditableEntity, IAggregateRoot
{
    public ProductDetail(Guid productId, Guid detailId)
    {
        ProductId = productId;
        DetailId = detailId;
    }

    public Guid ProductId { get; set; }
    public Guid DetailId { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual Detail Detail { get; set; } = default!;

    public ProductDetail Update(Guid? productId, Guid? detailId)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (detailId.HasValue && detailId.Value != Guid.Empty && !DetailId.Equals(detailId.Value))
            DetailId = detailId.Value;
        return this;
    }
}