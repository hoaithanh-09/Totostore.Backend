namespace Totostore.Backend.Domain.Catalog;

public class Customer : AuditableEntity, IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public virtual List<Notification> Notifications { get; set; } = default!;
    public virtual List<Cart> Carts { get; set; } = default!;
    public virtual List<Order> Orders { get; set; } = default!;
    public virtual List<Address> Addresses { get; set; } = default!;
}