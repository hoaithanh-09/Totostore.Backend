namespace Totostore.Backend.Application.Catalog.Addresses;

public class AddressDto : IDto
{
    public Guid Id { get; set; }
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Ward { get; set; } = default!;
    public string StayingAddress { get; set; } = default!;
}