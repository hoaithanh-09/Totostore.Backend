namespace Totostore.Backend.Application.Catalog.Notifications;

public class NotificationDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string UserId { get; set; }
}