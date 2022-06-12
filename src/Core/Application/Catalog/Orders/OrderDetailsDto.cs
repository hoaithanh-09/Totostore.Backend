using Totostore.Backend.Application.Catalog.Addresses;
using Totostore.Backend.Application.Catalog.Customers;
using Totostore.Backend.Application.Catalog.Shippers;

namespace Totostore.Backend.Application.Catalog.Orders;

public class OrderDetailsDto : IDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public Guid AddressDeliveryId { get; set; }
    public CustomerDto Customer { get; set; } = default!;
    public ShipperDto Shipper { get; set; } = default!;
    public AddressDto AddressDelivery { get; set; } = default!;
}