using Totostore.Backend.Application.Catalog.ProductImages;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class ProductImagesController:VersionedApiController
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
  //  [MustHavePermission(FSHAction.View, FSHResource.ProductImages)]
    [OpenApiOperation("Get productImages details.", "")]
    public Task<ProductImageDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetProductImageRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.ProductImages)]
    [OpenApiOperation("Create a new productImages.", "")]
    public Task<Guid> CreateAsync(CreateProductImageRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.ProductImages)]
    [OpenApiOperation("Delete a productImages.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProductImageRequest(id));
    }
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
  //  [MustHavePermission(FSHAction.Search, FSHResource.ProductImages)]
    [OpenApiOperation("Search productImages using available filters.", "")]
    public Task<PaginationResponse<ProductImageDto>> SearchAsync(SearchProductImagesRequest request)
    {
        return Mediator.Send(request);
    }

}