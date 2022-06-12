using Totostore.Backend.Application.Catalog.Orders;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderStatuses;

public class OrderStatusDetailsDto : IDto
{
    public Guid Id { get; set; }
    public OrderStatusEnums Status { get; set; }
    public string? Note { get; set; } = default!;
    public Guid OrderId { get; set; }
    public OrderDto Order { get; set; } = default!;
}