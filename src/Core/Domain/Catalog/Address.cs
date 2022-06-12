namespace Totostore.Backend.Domain.Catalog;

public class Address : AuditableEntity, IAggregateRoot
{
    public Address(string city, string district, string ward, string stayingAddress)
    {
        City = city;
        District = district;
        Ward = ward;
        StayingAddress = stayingAddress;
    }

    public string City { get; set; }
    public string District { get; set; }
    public string Ward { get; set; }
    public string StayingAddress { get; set; }
    public virtual List<Shipper> Shippers { get; set; } = default!;
    public virtual List<Customer> Customers { get; set; } = default!;
    public virtual List<Order> Orders { get; set; } = default!;
    public virtual List<Supplier> Suppliers { get; set; } = default!;

    public Address Update(string? city, string? district, string? ward, string? stayingAddress)
    {
        if (city is not null && City?.Equals(city) is not true) City = city;
        if (district is not null && District?.Equals(district) is not true) District = district;
        if (ward is not null && Ward?.Equals(ward) is not true) Ward = ward;
        if (stayingAddress is not null && StayingAddress?.Equals(stayingAddress) is not true)
            StayingAddress = stayingAddress;
        return this;
    }
}