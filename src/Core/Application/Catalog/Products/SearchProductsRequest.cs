using Totostore.Backend.Application.Catalog.CategoryProducts;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Products;

public class SearchProductsRequest : PaginationFilter, IRequest<PaginationResponse<Product>>
{
    public Guid? CategoryId { get; set; }
    public Status? Status { get; set; }
    public Guid? SupplierId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}

public class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, PaginationResponse<Product>>
{
    private readonly IReadRepository<Product> _productRepo;
    private readonly IReadRepository<CategoryProduct> _categoryProductRepo;

    public SearchProductsRequestHandler(IReadRepository<Product> productRepo,
        IReadRepository<CategoryProduct> categoryProductRepo)
        => (_productRepo, _categoryProductRepo) = (productRepo, categoryProductRepo);

    public async Task<PaginationResponse<Product>> Handle(SearchProductsRequest request,
        CancellationToken cancellationToken)
    {
        var query = await _productRepo.ListAsync();
        if (request.CategoryId.HasValue)
        {
            var categoryProducts = await _categoryProductRepo
                .ListAsync(new ProductsByCategorySpec(request.CategoryId.Value), cancellationToken);
            query = categoryProducts.Select(x => x.Product).ToList();
        }
        else if (request.SupplierId.HasValue)
        {
            query = query.Where(x => x.SupplierId != null && x.SupplierId == request.SupplierId.Value)
                .OrderBy(x => x.Name).ToList();
        }
        else if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value).OrderBy(x => x.Name).ToList();
        }
        else if (request.MinimumRate.HasValue)
        {
            query = query.Where(x => x.Rate >= request.MinimumRate.Value).OrderBy(x => x.Name).ToList();
        }
        else if (request.MaximumRate.HasValue)
        {
            query = query.Where(x => x.Rate <= request.MaximumRate.Value).OrderBy(x => x.Name).ToList();
        }
        else
        {
            query = query.OrderBy(x => x.Name).ToList();
        }

        return await _productRepo.NewPaginatedListAsync(query, request.PageNumber, request.PageSize,
            cancellationToken: cancellationToken);
    }
}