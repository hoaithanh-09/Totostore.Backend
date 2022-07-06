namespace Totostore.Backend.Domain.Catalog;

public class Notification : AuditableEntity, IAggregateRoot
{
    public Notification(string title, string content, Guid customerId)
    {
        Title = title;
        Content = content;
        CustomerId = customerId;
    }

    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = default!;

    public Notification Update(string? title, string? content, Guid? customerId)
    {
        if (title is not null && Title?.Equals(title) is not true) Title = title;
        if (content is not null && Content?.Equals(content) is not true) Content = content;
        if (customerId.HasValue && customerId.Value != Guid.Empty && !CustomerId.Equals(customerId.Value))
            CustomerId = customerId.Value;
        return this;
    }
}