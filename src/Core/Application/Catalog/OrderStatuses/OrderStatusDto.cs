using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderStatuses;

public class OrderStatusDto : IDto
{
    public Guid Id { get; set; }
    public OrderStatusEnums Status { get; set; }
    public string Note { get; set; } = default!;
    public Guid OrderId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string OrderName { get; set; } = default!;
}