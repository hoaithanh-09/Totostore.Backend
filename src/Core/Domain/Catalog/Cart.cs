using Totostore.Backend.Domain.Identity;

namespace Totostore.Backend.Domain.Catalog;

public class Cart : AuditableEntity, IAggregateRoot
{
    public Cart(Guid productId, string userId, int quantity, decimal price)
    {
        ProductId = productId;
        UserId = userId;
        Quantity = quantity;
        Price = price;
    }

    public Guid ProductId { get; set; }
    public string UserId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public virtual ApplicationUser User { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;

    public Cart Update(Guid? productId, string? userId, int? quantity, decimal? price)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (userId is not null && UserId?.Equals(UserId) is not true) UserId = userId;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        if (price.HasValue && Price != price) Price = price.Value;
        return this;
    }
}