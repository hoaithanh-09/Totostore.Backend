using Totostore.Backend.Domain.Identity;

namespace Totostore.Backend.Domain.Catalog;

public class Notification : AuditableEntity, IAggregateRoot
{
    public Notification(string title, string content, string userId)
    {
        Title = title;
        Content = content;
        UserId = userId;
    }

    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public Notification Update(string? title, string? content, string? userId)
    {
        if (title is not null && Title?.Equals(title) is not true) Title = title;
        if (content is not null && Content?.Equals(content) is not true) Content = content;
        if (userId is not null && UserId?.Equals(userId) is not true) UserId = userId;
        return this;
    }
}