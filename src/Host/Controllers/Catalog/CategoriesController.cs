using Totostore.Backend.Application.Catalog.Categories;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class CategoriesController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
   // [MustHavePermission(FSHAction.Search, FSHResource.Categories)]
    [OpenApiOperation("Search categories using available filters.", "")]
    public Task<PaginationResponse<CategoryDto>> SearchAsync(SearchCategoriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
   // [MustHavePermission(FSHAction.View, FSHResource.Categories)]
    [OpenApiOperation("Get category details.", "")]
    public Task<CategoryDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCategoryRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Categories)]
    [OpenApiOperation("Create a new category.", "")]
    public Task<Guid> CreateAsync(CreateCategoryRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Categories)]
    [OpenApiOperation("Update a category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCategoryRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //  [MustHavePermission(FSHAction.Delete, FSHResource.Categories)]
    [OpenApiOperation("Delete a category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCategoryRequest(id));
    }
}