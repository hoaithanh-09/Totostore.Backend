namespace Totostore.Backend.Application.Catalog.Orders;

public class OrderDto:IDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShipperId { get; set; }
    public Guid AddressDeliveryId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string CustomerName { get; set; }
    public string ShipperName { get; set; }

}