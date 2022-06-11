using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderPayments;

public class OrderPaymentDto : IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string OrderName { get; set; } = default!;
    public string PaymentName { get; set; } = default!;
}