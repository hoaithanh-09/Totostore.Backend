namespace Totostore.Backend.Domain.Catalog;

public class Shipper : AuditableEntity, IAggregateRoot
{
    public Shipper(string name, DateTime dob, bool gender, string mail, string phoneNumber, Guid addressId)
    {
        Name = name;
        Dob = dob;
        Mail = mail;
        PhoneNumber = phoneNumber;
        AddressId = addressId;
    }

    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
    public virtual Address Address { get; set; } = default!;
    public virtual List<Order> Orders { get; set; } = default!;
    public Shipper Update(string? name, DateTime? dob, bool? gender, string? mail, string? phoneNumber, Guid? addressId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (dob.HasValue && Dob != dob) Dob = dob.Value;
        if (gender.HasValue && Gender != gender) Gender = gender.Value;
        if (mail is not null && Mail?.Equals(mail) is not true) Mail = mail;
        if (phoneNumber is not null && PhoneNumber?.Equals(phoneNumber) is not true) PhoneNumber = phoneNumber;
        if (addressId.HasValue && addressId.Value != Guid.Empty && !AddressId.Equals(addressId.Value))
            AddressId = addressId.Value;
        return this;
    }
}