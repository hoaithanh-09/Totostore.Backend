namespace Totostore.Backend.Application.Catalog.Orders;

public class UpdateOrderRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public Guid AddressDeliveryId { get; set; }
}