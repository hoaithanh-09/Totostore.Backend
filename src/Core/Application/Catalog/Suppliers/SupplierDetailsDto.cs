using Totostore.Backend.Application.Catalog.Addresses;

namespace Totostore.Backend.Application.Catalog.Suppliers;

public class SupplierDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? AddressId { get; set; }
    public AddressDto Address { get; set; }
}