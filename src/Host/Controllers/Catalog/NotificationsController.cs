using Totostore.Backend.Application.Catalog.Notifications;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class NotificationsController : VersionedApiController
{
    private readonly INotificationService _notificationService;
    public NotificationsController(INotificationService notificationService) => _notificationService = notificationService;

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Notifications)]
    [OpenApiOperation("Search notifications using available filters.", "")]
    public Task<PaginationResponse<NotificationDto>> SearchAsync(SearchNotificationsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("InsertForCustomer")]
    [MustHavePermission(FSHAction.Create, FSHResource.Notifications)]
    [OpenApiOperation("Create a new notification.", "")]
    public Task CreateAsync(CreateGeneralNotificationForIdsRequest request)
    {
        return _notificationService.CreateNotificationForCustomers(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Notifications)]
    [OpenApiOperation("Get notification details.", "")]
    public Task<NotificationDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetNotificationRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Notifications)]
    [OpenApiOperation("Create a new notification.", "")]
    public Task<Guid> CreateAsync(CreateNotificationRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Notifications)]
    [OpenApiOperation("Update a notification.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateNotificationRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Notifications)]
    [OpenApiOperation("Delete a notification.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteNotificationRequest(id));
    }
}