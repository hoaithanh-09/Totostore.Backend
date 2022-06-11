namespace Totostore.Backend.Domain.Catalog;

public class OrderProduct : AuditableEntity, IAggregateRoot
{
    public OrderProduct(Guid productId, Guid orderId, Guid productPriceId, int quantity)
    {
        ProductId = productId;
        OrderId = orderId;
        ProductPriceId = productPriceId;
        Quantity = quantity;
    }

    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductPriceId { get; set; }
    public int Quantity { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual Order Order { get; set; } = default!;
    public virtual ProductPrice ProductPrice { get; set; } = default!;

    public OrderProduct Update(Guid? productId, Guid? orderId, Guid? productPriceId, int? quantity)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (orderId.HasValue && orderId.Value != Guid.Empty && !OrderId.Equals(orderId.Value)) OrderId = orderId.Value;
        if (productPriceId.HasValue && productPriceId.Value != Guid.Empty &&
            !ProductPriceId.Equals(productPriceId.Value)) ProductPriceId = productPriceId.Value;
        if (quantity.HasValue && Quantity != quantity) quantity = quantity.Value;
        return this;
    }
}