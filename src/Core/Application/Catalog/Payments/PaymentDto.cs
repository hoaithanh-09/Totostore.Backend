namespace Totostore.Backend.Application.Catalog.Payments;

public class PaymentDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime DateCreated { get; set; } = DateTime.Now;
}