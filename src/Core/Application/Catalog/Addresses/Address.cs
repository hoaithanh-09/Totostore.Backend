namespace Totostore.Backend.Application.Catalog.Addresses;

public class AddressDto : IDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
}