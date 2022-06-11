namespace Totostore.Backend.Application.Catalog.Shippers;

public class ShipperDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
}