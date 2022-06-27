using MapsterMapper;
using Totostore.Backend.Application.Catalog.CategoryProducts;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Products;

public class SearchProductsRequest : PaginationFilter, IRequest<PaginationResponse<ProductViewModel>>
{
    public List<Guid>? CategoryIds { get; set; }
    public Status? Status { get; set; }
    public Guid? SupplierId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }

}

public class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, PaginationResponse<ProductViewModel>>
{
    private readonly IReadRepository<Product> _productRepo;
    private readonly IReadRepository<CategoryProduct> _categoryProductRepo;
    private readonly IMapper _mapper;

    public SearchProductsRequestHandler(IReadRepository<Product> productRepo, IMapper mapper,
        IReadRepository<CategoryProduct> categoryProductRepo)
        => (_productRepo, _categoryProductRepo, _mapper) = (productRepo, categoryProductRepo, mapper);

    public async Task<PaginationResponse<ProductViewModel>> Handle(SearchProductsRequest request,
        CancellationToken cancellationToken)
    {
        var query = await _productRepo.ListAsync();
        if (request.CategoryIds.Count()!=0)
        {
            int count = request.CategoryIds.Count();
            for (var i = 1; i <= count; i++)
            {
                var categoryProducts = await _categoryProductRepo
                    .ListAsync(new ProductsByCategorySpec(request.CategoryIds[i]), cancellationToken);
                query = categoryProducts.Select(x => x.Product).ToList();
            }
        }
         if (request.SupplierId.HasValue)
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

        var result = _mapper.Map<List<Product>, List<ProductViewModel>>(query.ToList());
        return await _productRepo.NewPaginatedListAsync(result, request.PageNumber, request.PageSize,
            cancellationToken: cancellationToken);
    }
}