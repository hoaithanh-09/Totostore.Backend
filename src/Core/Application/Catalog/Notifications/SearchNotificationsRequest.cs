namespace Totostore.Backend.Application.Catalog.Notifications;

public class SearchNotificationsRequest : PaginationFilter, IRequest<PaginationResponse<NotificationDto>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class NotificationsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Notification, NotificationDto>
{
    public NotificationsBySearchRequestSpec(SearchNotificationsRequest request)
        : base(request) =>
        Query.Where(x =>
            x.CreatedOn.Date.CompareTo(request.FromDate.Value) >= 0 &&
            x.CreatedOn.Date.CompareTo(request.ToDate.Value) <= 0);
}

public class
    SearchNotificationsRequestHandler : IRequestHandler<SearchNotificationsRequest, PaginationResponse<NotificationDto>>
{
    private readonly IReadRepository<Notification> _repository;

    public SearchNotificationsRequestHandler(IReadRepository<Notification> repository) => _repository = repository;

    public async Task<PaginationResponse<NotificationDto>> Handle(SearchNotificationsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new NotificationsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize,
            cancellationToken: cancellationToken);
    }
}