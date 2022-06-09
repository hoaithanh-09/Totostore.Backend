namespace Totostore.Backend.Application.Catalog.Suppliers;

public class SupplierDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid AddressId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}