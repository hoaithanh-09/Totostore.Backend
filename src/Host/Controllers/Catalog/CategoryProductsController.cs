using Totostore.Backend.Application.Catalog.CategoryProducts;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class CategoryProductsController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.CategoryProducts)]
    [OpenApiOperation("Get categoryProduct details.", "")]
    public Task<CategoryProductDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCategoryProductRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.CategoryProducts)]
    [OpenApiOperation("Create a new categoryProduct.", "")]
    public Task<Guid> CreateAsync(CreateCategoryProductRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.CategoryProducts)]
    [OpenApiOperation("Delete a categoryProduct.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCategoryProductRequest(id));
    }
}