using Totostore.Backend.Application.Catalog.Customers;
using Totostore.Backend.Application.Catalog.Shippers;
using Totostore.Backend.Application.Identity.Users;

namespace Totostore.Backend.Application.Catalog.Notifications;

public class NotificationDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string UserId { get; set; }
    public UserDetailsDto User { get; set; }
}