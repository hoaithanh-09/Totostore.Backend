namespace Totostore.Backend.Domain.Catalog;

public class Cart : AuditableEntity, IAggregateRoot
{
    public Cart(Guid productId, Guid customerId, int quantity, decimal price)
    {
        ProductId = productId;
        CustomerId = customerId;
        Quantity = quantity;
        Price = price;
    }

    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public virtual Customer Customer { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;

    public Cart Update(Guid? productId, Guid? customerId, int? quantity, decimal? price)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (customerId.HasValue && customerId.Value != Guid.Empty && !CustomerId.Equals(customerId.Value))
            CustomerId = customerId.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        if (price.HasValue && Price != price) Price = price.Value;
        return this;
    }
}