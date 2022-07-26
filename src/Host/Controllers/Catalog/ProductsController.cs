using Totostore.Backend.Application.Catalog.Products;
using Totostore.Backend.Domain.Catalog;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class ProductsController : VersionedApiController
{
    //private readonly IProductService _productService;
    //public ProductsController(IProductService productService) => _productService = productService;

    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Products)]
    [OpenApiOperation("Search products using available filters.", "")]
    public Task<PaginationResponse<ProductDetailsDto>> SearchAsync(SearchProductsRequest request)
    {
        return Mediator.Send(request);
    }

    //[HttpPost("Search-list")]
    //[MustHavePermission(FSHAction.Search, FSHResource.Products)]
    //[OpenApiOperation("Get list of all users.", "")]
    //public Task<PaginationResponse<ProductDetailsDto>> SearchAsync(SearchProductsRequest request, CancellationToken cancellationToken)
    //{
    //    return _productService.SearchAsync(request,cancellationToken);
    //}

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
   // [MustHavePermission(FSHAction.View, FSHResource.Products)]
    [OpenApiOperation("Get product details.", "")]
    public Task<ProductDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetProductRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Products)]
    [OpenApiOperation("Get product details via dapper.", "")]
    public Task<ProductDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetProductViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Products)]
    [OpenApiOperation("Create a new product.", "")]
    public Task<Guid> CreateAsync(CreateProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Products)]
    [OpenApiOperation("Update a product.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProductRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    
    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Products)]
    [OpenApiOperation("Delete a product.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProductRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Products)]
    [OpenApiOperation("Export a products.", "")]
    public async Task<FileResult> ExportAsync(ExportProductsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ProductExports");
    }
    }