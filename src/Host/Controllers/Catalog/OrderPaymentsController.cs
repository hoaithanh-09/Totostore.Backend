using Totostore.Backend.Application.Catalog.OrderPayments;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class OrderPaymentsController:VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.OrderPayments)]
    [OpenApiOperation("Get orderPayments details.", "")]
    public Task<OrderPaymentDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderPaymentRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.OrderPayments)]
    [OpenApiOperation("Create a new orderPaymet.", "")]
    public Task<Guid> CreateAsync(CreateOrderPaymentRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.OrderPayments)]
    [OpenApiOperation("Delete a orderPaymet.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrderPaymentRequest(id));
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.OrderPayments)]
    [OpenApiOperation("Search orderPayments using available filters.", "")]
    public Task<PaginationResponse<OrderPaymentDto>> SearchAsync(SearchOrderPaymentsRequest request)
    {
        return Mediator.Send(request);
    }
}