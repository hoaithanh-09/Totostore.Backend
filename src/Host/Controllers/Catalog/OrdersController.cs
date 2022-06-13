using Totostore.Backend.Application.Catalog.Orders;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class OrdersController:VersionedApiController
{
    // [HttpPost("search")]
    // [MustHavePermission(FSHAction.Search, FSHResource.Orders)]
    // [OpenApiOperation("Search orders using available filters.", "")]
    // public Task<PaginationResponse<OrderDto>> SearchAsync(SearchOrderRequest request)
    // {
    //     return Mediator.Send(request);
    // }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Orders)]
    [OpenApiOperation("Get order details.", "")]
    public Task<OrderDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Orders)]
    [OpenApiOperation("Create a new order.", "")]
    public Task<Guid> CreateAsync(CreateOrderRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Orders)]
    [OpenApiOperation("Update a order.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateOrderRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Orders)]
    [OpenApiOperation("Delete a order.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrderRequest(id));
    }
}