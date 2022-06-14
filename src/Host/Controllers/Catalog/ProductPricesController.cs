using Totostore.Backend.Application.Catalog.ProductPrices;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class ProductPricesController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.ProductPrices)]
    [OpenApiOperation("Get productPrice details.", "")]
    public Task<ProductPriceDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetProductPriceRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.ProductPrices)]
    [OpenApiOperation("Create a new productPrice.", "")]
    public Task<Guid> CreateAsync(CreateProductPriceRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.ProductPrices)]
    [OpenApiOperation("Delete a productPrice.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProductPriceRequest(id));
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.ProductPrices)]
    [OpenApiOperation("Update a productPrice.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProductPriceRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }


    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.ProductPrices)]
    [OpenApiOperation("Search productPrices using available filters.", "")]
    public Task<PaginationResponse<ProductPriceDto>> SearchAsync(SearchProductPricesRequest request)
    {
        return Mediator.Send(request);
    }
}