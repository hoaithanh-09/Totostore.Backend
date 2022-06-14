using Totostore.Backend.Application.Catalog.ProductDetails;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class ProductDetailsController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.ProductDetails)]
    [OpenApiOperation("Get productDetail details.", "")]
    public Task<ProductDetailDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetProductDetailRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CategoryProducts)]
    [OpenApiOperation("Create a new productDetail.", "")]
    public Task<Guid> CreateAsync(CreateProductDetailRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CategoryProducts)]
    [OpenApiOperation("Delete a productDetail.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProductDetailRequest(id));
    }
}