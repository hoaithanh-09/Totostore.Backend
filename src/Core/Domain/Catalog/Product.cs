using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class Product : AuditableEntity, IAggregateRoot
{
    public Product(string name, string slug, string? description, decimal rate, int quantity, Status status,
        Guid supplierId)
    {
        Name = name;
        Slug = slug;
        Description = description;
        Rate = rate;
        Quantity = quantity;
        Status = status;
        SupplierId = supplierId;
    }

    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public Status Status { get; set; }
    public Guid SupplierId { get; set; }
    public virtual Supplier Supplier { get; private set; } = default!;
    public virtual List<ProductDetail> ProductDetails { get; set; } = default!;
    public virtual List<ProductImage> ProductImages { get; set; } = default!;
    public virtual List<ProductPrice> ProductPrices { get; set; } = default!;
    public virtual List<CategoryProduct> CategoryProducts { get; set; } = default!;

    public virtual List<Cart> Carts { get; set; } = default!;
    public virtual List<OrderProduct> OrderProducts { get; set; } = default!;

    public Product Update(string? name, string? slug, string? description, decimal? rate, int? quantity, Status? status,
        Guid? supplierId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (slug is not null && Slug?.Equals(slug) is not true) Slug = slug;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (rate.HasValue && Rate != rate) Rate = rate.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        if (status.HasValue && Status != status) Status = status.Value;
        if (supplierId.HasValue && supplierId.Value != Guid.Empty && !SupplierId.Equals(supplierId.Value))
            SupplierId = supplierId.Value;
        return this;
    }
}