namespace Totostore.Backend.Domain.Catalog;

public class Cart : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public virtual Customer Customer { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;

    public Cart(Guid productId, Guid customerId, int quantity, decimal price)
    {
        ProductId = productId;
        CustomerId = customerId;
        Quantity = quantity;
        Price = price;
    }
}