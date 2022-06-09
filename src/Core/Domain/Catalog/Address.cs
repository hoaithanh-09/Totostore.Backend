namespace Totostore.Backend.Domain.Catalog;

public class Address : AuditableEntity, IAggregateRoot
{
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = default!;
    public virtual List<Shipper> Shippers { get; set; } = default!;
    public virtual List<Order> Orders { get; set; } = default!;
    public virtual List<Supplier> Suppliers { get; set; } = default!;

    public Address(Guid customerId)
    {
        customerId = customerId;
    }
    public Address Update(Guid? customerId)
    {
        if (customerId.HasValue && customerId.Value != Guid.Empty && !CustomerId.Equals(customerId.Value)) CustomerId = customerId.Value;
        return this;
    }
}
