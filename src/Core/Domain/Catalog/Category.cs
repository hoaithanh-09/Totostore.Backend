namespace Totostore.Backend.Domain.Catalog;

public class Category : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    public int ParentId { get; set; }
    public bool IsShowed { get; set; }
    public virtual List<CategoryProduct> CategoryProducts { get; set; }
}