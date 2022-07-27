using Totostore.Backend.Domain.Identity;

namespace Totostore.Backend.Domain.Catalog;

public class Customer : AuditableEntity, IAggregateRoot
{
    public Customer(string name, DateTime dob, bool gender, string mail, string phoneNumber, Guid addressId, string userId)
    {
        Name = name;
        Dob = dob;
        Gender = gender;
        Mail = mail;
        PhoneNumber = phoneNumber;
        AddressId = addressId;
        UserId = userId;
    }

    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; } 
    public virtual Address Address { get; set; } = default!;
    public virtual List<Order> Orders { get; set; } = new();

    public Customer Update(string? name, DateTime? dob, bool? gender, string? mail, string? phoneNumber,
        Guid? addressId, string? userId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (dob.HasValue && Dob != dob) Dob = dob.Value;
        if (gender.HasValue && Gender != gender) Gender = gender.Value;
        if (mail is not null && Mail?.Equals(mail) is not true) Mail = mail;
        if (phoneNumber is not null && PhoneNumber?.Equals(phoneNumber) is not true) PhoneNumber = phoneNumber;
        if (addressId.HasValue && addressId.Value != Guid.Empty && !AddressId.Equals(addressId.Value))
            AddressId = addressId.Value;
        if (userId is not null && User?.Equals(UserId) is not true) UserId = userId;
        return this;
    }

}