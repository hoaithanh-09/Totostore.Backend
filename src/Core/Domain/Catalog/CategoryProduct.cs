namespace Totostore.Backend.Domain.Catalog;

public class CategoryProduct : AuditableEntity, IAggregateRoot
{
    public CategoryProduct(Guid productId, Guid categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual CategoryDetailDto Category { get; set; } = default!;

    public CategoryProduct Update(Guid? productId, Guid? categoryId)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (categoryId.HasValue && categoryId.Value != Guid.Empty && !CategoryId.Equals(categoryId.Value))
            CategoryId = categoryId.Value;
        return this;
    }
}