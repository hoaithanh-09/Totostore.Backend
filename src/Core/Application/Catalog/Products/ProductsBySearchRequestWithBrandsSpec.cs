namespace Totostore.Backend.Application.Catalog.Products;

public class ProductsBySearchRequestWithBrandsSpec : EntitiesByPaginationFilterSpec<Product, ProductDto>
{
    public ProductsBySearchRequestWithBrandsSpec(SearchProductsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Supplier)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.SupplierId.Equals(request.SupplierId!.Value), request.SupplierId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}