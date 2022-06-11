namespace Totostore.Backend.Application.Catalog.Notifications;

public class GetNotificationRequest : IRequest<NotificationDetailsDto>
{
    public GetNotificationRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class NotificationByIdSpec : Specification<Notification, NotificationDetailsDto>, ISingleResultSpecification
{
    public NotificationByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetNotificationRequestHandler : IRequestHandler<GetNotificationRequest, NotificationDetailsDto>
{
    private readonly IStringLocalizer<GetNotificationRequestHandler> _localizer;
    private readonly IRepository<Notification> _repository;

    public GetNotificationRequestHandler(IRepository<Notification> repository,
        IStringLocalizer<GetNotificationRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<NotificationDetailsDto> Handle(GetNotificationRequest request,
        CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Notification, NotificationDetailsDto>)new NotificationByIdSpec(request.Id),
            cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["notification.notfound"], request.Id));
}