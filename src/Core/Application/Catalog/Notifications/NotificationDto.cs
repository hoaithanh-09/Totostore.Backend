namespace Totostore.Backend.Application.Catalog.Notifications;

public class NotificationDto: IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string? CustomerName { get; set; }
    public string? ShipperName { get; set; }
}