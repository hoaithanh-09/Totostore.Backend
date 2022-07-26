using Totostore.Backend.Application.Catalog.Details;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class DetailsController:VersionedApiController
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
  //  [MustHavePermission(FSHAction.View, FSHResource.Details)]
    [OpenApiOperation("Get detail details.", "")]
    public Task<DetailDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDetailRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Details)]
    [OpenApiOperation("Create a new detail.", "")]
    public Task<Guid> CreateAsync(CreateDetailRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Details)]
    [OpenApiOperation("Update a detail.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDetailRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Details)]
    [OpenApiOperation("Delete a detail.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteDetailRequest(id));
    }

    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
  //[MustHavePermission(FSHAction.Search, FSHResource.Details)]
    [OpenApiOperation("Search details using available filters.", "")]
    public Task<PaginationResponse<DetailDto>> SearchAsync(SearchDetailsRequest request)
    {
        return Mediator.Send(request);
    }
}