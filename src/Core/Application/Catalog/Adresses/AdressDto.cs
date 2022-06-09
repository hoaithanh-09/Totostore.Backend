namespace Totostore.Backend.Application.Catalog.Adresses;

public class AdressDto : IDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; } = default!;
    public string CustomerName { get; set; } = default!;

}