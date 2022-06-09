namespace Totostore.Backend.Domain.Catalog;

public class CategoryProduct : AuditableEntity, IAggregateRoot
{
    public int ProductId { get; set;}
    public int CategoryId { get; set; }
    public virtual Product Product { get; set; }
    public virtual Category Category { get; set; }
}