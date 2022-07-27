namespace Totostore.Backend.Domain.Catalog;

public class Address : AuditableEntity, IAggregateRoot
{
    public Address(string cityCode, string city, string districtCode, string district, string wardCode, string ward, string stayingAddress)
    {
        CityCode = cityCode;
        City = city;
        DistrictCode = districtCode;
        District = district;
        WardCode = wardCode;
        Ward = ward;
        StayingAddress = stayingAddress;
    }
    public string CityCode { get; set; }
    public string City { get; set; }
    public string DistrictCode { get; set; }
    public string District { get; set; }
    public string WardCode { get;set; }
    public string Ward { get; set; }
    public string StayingAddress { get; set; }
    public virtual List<Shipper> Shippers { get; set; } = new();
    public virtual List<Customer> Customers { get; set; } = new();
    public virtual List<Order> Orders { get; set; } = new();
    public virtual List<Supplier> Suppliers { get; set; } = new();

    public Address Update(string? cityCode,string? city, string districtCode, string? district, string? wardCode, string? ward, string? stayingAddress)
    {
        if (cityCode is not null && CityCode?.Equals(cityCode) is not true) CityCode = cityCode;
        if (city is not null && City?.Equals(city) is not true) City = city;
        if (districtCode is not null && DistrictCode?.Equals(districtCode) is not true) DistrictCode = districtCode;
        if (district is not null && District?.Equals(district) is not true) District = district;
        if (wardCode is not null && WardCode?.Equals(wardCode) is not true) WardCode = wardCode;
        if (ward is not null && Ward?.Equals(ward) is not true) Ward = ward;
        if (stayingAddress is not null && StayingAddress?.Equals(stayingAddress) is not true)
            StayingAddress = stayingAddress;
        return this;
    }
}