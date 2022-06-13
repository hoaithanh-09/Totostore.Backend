using Totostore.Backend.Application.Catalog.Shippers;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class ShippersController:VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Shippers)]
    [OpenApiOperation("Search shippers using available filters.", "")]
    public Task<PaginationResponse<ShipperDto>> SearchAsync(SearchShippersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Shippers)]
    [OpenApiOperation("Get shipper details.", "")]
    public Task<ShipperDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetShipperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Shippers)]
    [OpenApiOperation("Create a new shipper.", "")]
    public Task<Guid> CreateAsync(CreateShipperRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Shippers)]
    [OpenApiOperation("Update a shipper.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateShipperRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Shippers)]
    [OpenApiOperation("Delete a shipper.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteShipperRequest(id));
    }
}