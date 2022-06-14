using Totostore.Backend.Application.Catalog.OrderCoupons;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class OrderCouponController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.OrderCoupons)]
    [OpenApiOperation("Get orderCoupons details.", "")]
    public Task<OrderCouponDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderCouponRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.OrderCoupons)]
    [OpenApiOperation("Create a new orderCoupon.", "")]
    public Task<Guid> CreateAsync(CreateOrderCouponRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.OrderCoupons)]
    [OpenApiOperation("Delete a orderCoupon.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrderCouponRequest(id));
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.OrderCoupons)]
    [OpenApiOperation("Search orderCoupons using available filters.", "")]
    public Task<PaginationResponse<OrderCouponDto>> SearchAsync(SearchOrderCouponsRequest request)
    {
        return Mediator.Send(request);
    }
}