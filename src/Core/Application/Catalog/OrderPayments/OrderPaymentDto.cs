using Totostore.Backend.Application.Common.Enums;

namespace Totostore.Backend.Application.Catalog.OrderPayments;

public class OrderPaymentDto:IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string OrderName { get; set; }
    public string PaymentName { get; set; }
}