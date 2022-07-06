using Totostore.Backend.Application.Catalog.Customers;
using Totostore.Backend.Application.Catalog.Shippers;

namespace Totostore.Backend.Application.Catalog.Notifications;

public class NotificationDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public CustomerDto Customer { get; set; }
}