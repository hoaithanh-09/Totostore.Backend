using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Notifications;

public class SearchNotificationsRequest : PaginationFilter, IRequest<PaginationResponse<NotificationDto>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? UserId { get; set; }
    public NotificationType? Type { get; set; }
}

