using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Notifications;

public class UpdateNotificationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string UserId { get; set; }
}

public class UpdateNotificationRequestValidator : CustomValidator<UpdateNotificationRequest>
{
    public UpdateNotificationRequestValidator(IReadRepository<Notification> repository,
        IStringLocalizer<UpdateNotificationRequestValidator> localizer)
    {
    }
}

public class UpdateNotificationRequestHandler : IRequestHandler<UpdateNotificationRequest, Guid>
{
    private readonly IStringLocalizer<UpdateNotificationRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Notification> _repository;

    public UpdateNotificationRequestHandler(IRepositoryWithEvents<Notification> repository,
        IStringLocalizer<UpdateNotificationRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateNotificationRequest request, CancellationToken cancellationToken)
    {
        var notification = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = notification ?? throw new NotFoundException(string.Format(_localizer["notification.notfound"], request.Id));

        notification.Update(request.Title, request.Content, request.UserId);
        // Add Domain Events to be raised after the commit
        notification.DomainEvents.Add(EntityUpdatedEvent.WithEntity(notification));

        await _repository.UpdateAsync(notification, cancellationToken);

        return request.Id;
    }
}