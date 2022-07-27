using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Notifications;
public class CreateGeneralNotificationForIdsRequest
{
    public List<string> Ids { get; set; }
    public NotificationViewModel Notification { get; set; }
}
public class NotificationViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NotificationType Type { get; set; }
}

public class InsertNotificationRequest
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string UserId { get; set; }
    public NotificationType Type { get; set; }
}
