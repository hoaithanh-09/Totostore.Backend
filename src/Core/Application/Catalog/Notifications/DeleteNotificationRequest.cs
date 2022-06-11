using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Notifications;

public class DeleteNotificationRequest : IRequest<Guid>
{
    public DeleteNotificationRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteNotificationRequestHandler : IRequestHandler<DeleteNotificationRequest, Guid>
{
    private readonly IStringLocalizer<DeleteNotificationRequestHandler> _localizer;
    private readonly IRepository<Notification> _repository;

    public DeleteNotificationRequestHandler(IRepository<Notification> repository,
        IStringLocalizer<DeleteNotificationRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteNotificationRequest request, CancellationToken cancellationToken)
    {
        var notification = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = notification ?? throw new NotFoundException(_localizer["notification.notfound"]);

        // Add Domain Events to be raised after the commit
        notification.DomainEvents.Add(EntityDeletedEvent.WithEntity(notification));

        await _repository.DeleteAsync(notification, cancellationToken);

        return request.Id;
    }
}