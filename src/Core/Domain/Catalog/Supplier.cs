namespace Totostore.Backend.Domain.Catalog;

public class Supplier : AuditableEntity, IAggregateRoot
{
    public Supplier(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public virtual List<Product> Products { get; set; } = new();

    public Supplier Update(string? name, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}