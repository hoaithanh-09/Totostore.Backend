namespace Totostore.Backend.Domain.Catalog;

public class Order : AuditableEntity, IAggregateRoot
{
    public Order(decimal amount, string? note, Guid customerId, Guid? shipperId, Guid addressDeliveryId)
    {
        Amount = amount;
        Note = note;
        CustomerId = customerId;
        ShipperId = shipperId;
        AddressDeliveryId = addressDeliveryId;
    }

    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public Guid AddressDeliveryId { get; set; }
    public virtual Customer Customer { get; private set; } = default!;
    public virtual Shipper Shipper { get; private set; } = default!;
    public virtual Address AddressDelivery { get; private set; } = default!;
    public virtual List<OrderCoupon> OrderCoupons { get; set; } = new();
    public virtual List<OrderPayment> OrderPayments { get; set; } = new();
    public virtual List<OrderProduct> OrderProducts { get; set; } = new();
    public virtual List<OrderStatus> OrderStatuses { get; set; } = new();

    public Order Update(decimal? amount, string? note, Guid? customerId, Guid? shipperId, Guid? addressDeliveryId)
    {
        if (amount.HasValue && Amount != amount) Amount = amount.Value;
        if (note is not null && Note?.Equals(note) is not true) Note = note;
        if (customerId.HasValue && customerId.Value != Guid.Empty && !CustomerId.Equals(customerId.Value))
            CustomerId = customerId.Value;
        if (shipperId.HasValue && shipperId.Value != Guid.Empty && !ShipperId.Equals(shipperId.Value))
            ShipperId = shipperId.Value;
        if (addressDeliveryId.HasValue && addressDeliveryId.Value != Guid.Empty &&
            !AddressDeliveryId.Equals(addressDeliveryId.Value)) AddressDeliveryId = addressDeliveryId.Value;
        return this;
    }
}