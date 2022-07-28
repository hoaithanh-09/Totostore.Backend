using Totostore.Backend.Application.Catalog.Carts;

namespace Totostore.Backend.Host.Controllers.Catalog;

[Authorize]
public class CartsController : VersionedApiController
{
    [HttpPost("search")]
  
    //[MustHavePermission(FSHAction.Search, FSHResource.Carts)]
    [OpenApiOperation("Search carts using available filters.", "")]
    public Task<PaginationResponse<CartDto>> SearchAsync(SearchCartsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]

   // [MustHavePermission(FSHAction.View, FSHResource.Carts)]
    [OpenApiOperation("Get cart details.", "")]
    public Task<CartDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCartRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Carts)]
    [OpenApiOperation("Create a new cart.", "")]
    public Task<Guid> CreateAsync(CreateCartRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
   // [MustHavePermission(FSHAction.Update, FSHResource.Carts)]
    [OpenApiOperation("Update a cart.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCartRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
   // [MustHavePermission(FSHAction.Delete, FSHResource.Carts)]
    [OpenApiOperation("Delete a cart.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCartRequest(id));
    }
}