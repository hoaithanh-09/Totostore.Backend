using Totostore.Backend.Application.Catalog.Coupons;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class CouponsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
  //  [MustHavePermission(FSHAction.Search, FSHResource.Coupons)]
    [OpenApiOperation("Search coupons using available filters.", "")]
    public Task<PaginationResponse<CouponDto>> SearchAsync(SearchCouponsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Coupons)]
    [OpenApiOperation("Get coupon details.", "")]
    public Task<CouponDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCouponRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Coupons)]
    [OpenApiOperation("Create a new coupon.", "")]
    public Task<Guid> CreateAsync(CreateCouponRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Coupons)]
    [OpenApiOperation("Update a coupon.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCouponRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Coupons)]
    [OpenApiOperation("Delete a coupon.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCouponRequest(id));
    }
}