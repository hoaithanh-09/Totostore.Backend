using MapsterMapper;
using Totostore.Backend.Application.Catalog.CategoryProducts;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Products;

public class SearchProductsRequest : PaginationFilter, IRequest<PaginationResponse<ProductDetailsDto>>
{
    public List<Guid>? CategoryIds { get; set; }
    public Status? Status { get; set; }
    public Guid? SupplierId { get; set; }
    public bool? IsSortRate { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? Rate { get; set; }

}

public class ProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Product, ProductDetailsDto>
{
    private readonly IReadRepository<CategoryProduct> _categoryProductRepo;
    private readonly IReadRepository<ProductPrice> _productPriceRepo;
    public ProductsBySearchRequestSpec(SearchProductsRequest request)
        : base(request)
    {
        Query.OrderBy(x => x.CreatedOn);
        if (request.Status.HasValue)
        {
            Query.Where(x => x.Status == request.Status.Value);
        }

        if (request.SupplierId.HasValue)
        {
            Query.Where(x => x.SupplierId == request.SupplierId.Value);
        }

        if (request.IsSortRate.HasValue && request.IsSortRate == true)
        {
            Query.OrderByDescending(x => x.Rate);
        }

        if (request.CategoryIds != null)
        {
            foreach (var item in request.CategoryIds)
            {
                var categoryProd = _categoryProductRepo
                    .ListAsync(new ProductsByCategorySpec(item))
                    .Result.Select(x => x.ProductId);
                Query.Where(x => categoryProd.Contains(x.Id));
            }
        }
        if (request.MinPrice.HasValue && request.MaxPrice.HasValue)
        {
            Query.Where(x => x.ProductPrices.OrderByDescending(x => x.CreatedOn).FirstOrDefault().Amount >= request.MinPrice.Value &&
                             x.ProductPrices.OrderByDescending(x => x.CreatedOn).FirstOrDefault().Amount <= request.MaxPrice.Value);
        }
        if(request.Rate.HasValue)
        {
            Query.Where(x => x.Rate >= request.Rate.Value && x.Rate <= 5);
        }

    }
}

public class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, PaginationResponse<ProductDetailsDto>>
{
    private readonly IReadRepository<Product> _productRepo;
    private readonly IReadRepository<CategoryProduct> _categoryProductRepo;
    private readonly IMapper _mapper;

    public SearchProductsRequestHandler(IReadRepository<Product> productRepo) => _productRepo = productRepo;

    public async Task<PaginationResponse<ProductDetailsDto>> Handle(SearchProductsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new ProductsBySearchRequestSpec(request);
        return await _productRepo.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}