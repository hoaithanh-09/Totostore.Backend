namespace Totostore.Backend.Domain.Catalog;

public class Shipper : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual Address Address { get; set; }
    public virtual List<Notification > Notifications { get; set; }
    public virtual List<Order> Orders { get; set; }
}