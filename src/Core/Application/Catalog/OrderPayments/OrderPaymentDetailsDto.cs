using Totostore.Backend.Application.Catalog.Orders;
using Totostore.Backend.Application.Catalog.Payments;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderPayments;

public class OrderPaymentDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public OrderDto Order { get; set; } = default!;
    public PaymentDto Payment { get; set; } = default!;
}