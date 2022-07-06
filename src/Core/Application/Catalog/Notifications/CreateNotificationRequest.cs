using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Notifications;

public class CreateNotificationRequest : IRequest<Guid>
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public Guid CustomerId { get; set; }
}

public class CreateNotificationRequestValidator : CustomValidator<CreateNotificationRequest>
{
    public CreateNotificationRequestValidator(IReadRepository<Customer> repository,
        IStringLocalizer<CreateNotificationRequestValidator> localizer)
    {
    }
}

public class CreateNotificationRequestHandler : IRequestHandler<CreateNotificationRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Notification> _repository;

    public CreateNotificationRequestHandler(IRepository<Notification> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateNotificationRequest request, CancellationToken cancellationToken)
    {
        var notification = new Notification(request.Title, request.Content, request.CustomerId);

        // Add Domain Events to be raised after the commit
        notification.DomainEvents.Add(EntityCreatedEvent.WithEntity(notification));

        await _repository.AddAsync(notification, cancellationToken);

        return notification.Id;
    }
}