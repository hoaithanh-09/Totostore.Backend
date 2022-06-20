using Totostore.Backend.Application.Catalog.Orders;
using Totostore.Backend.Application.Catalog.OrderStatus;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class OrderStatusController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.OrderStatus)]
    [OpenApiOperation("Get orderStatus details.", "")]
    public Task<OrderStatusDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderStatusRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.OrderStatus)]
    [OpenApiOperation("Create a new orderStatus.", "")]
    public Task<Guid> CreateAsync(CreateOrderStatusRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.OrderStatus)]
    [OpenApiOperation("Delete a orderStatus.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrderStatusRequest(id));
    }


    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.OrderStatus)]
    [OpenApiOperation("Update a orderStatus.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateOrderStatusRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }


    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.OrderStatus)]
    [OpenApiOperation("Search orderStatus using available filters.", "")]
    public Task<PaginationResponse<OrderStatusDto>> SearchAsync(SearchOrderStatusRequest request)
    {
        return Mediator.Send(request);
    }
}