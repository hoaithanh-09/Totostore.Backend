namespace Totostore.Backend.Domain.Catalog;

public class Payment : AuditableEntity, IAggregateRoot
{
    public Payment(string name)
    {
        Name = name;
    }

    public string Name { get; set; } = default!;
    public virtual List<OrderPayment> OrderPayments { get; set; } = default!;

    public Payment Update(string? name)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        return this;
    }
}