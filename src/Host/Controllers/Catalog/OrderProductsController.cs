using Totostore.Backend.Application.Catalog.OrderProducts;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class OrderProductsController:VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.OrderProducts)]
    [OpenApiOperation("Get orderProduct details.", "")]
    public Task<OrderProductDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderProductRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.OrderProducts)]
    [OpenApiOperation("Create a new orderProduct.", "")]
    public Task<Guid> CreateAsync(CreateOrderProductRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.OrderProducts)]
    [OpenApiOperation("Delete a orderProduct.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrderProductRequest(id));
    }
}