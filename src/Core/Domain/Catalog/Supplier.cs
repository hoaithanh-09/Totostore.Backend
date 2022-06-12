namespace Totostore.Backend.Domain.Catalog;

public class Supplier : AuditableEntity, IAggregateRoot
{
    public Supplier(string name, string? description, Guid? addressId)
    {
        Name = name;
        Description = description;
        AddressId = addressId;
    }

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? AddressId { get; set; }
    public virtual Address Address { get; set; } = default!;
    public virtual List<Product> Products { get; set; } = default!;

    public Supplier Update(string? name, string? description, Guid? addressId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (addressId.HasValue && addressId.Value != Guid.Empty && !AddressId.Equals(addressId.Value))
            AddressId = addressId.Value;
        return this;
    }
}