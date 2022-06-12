namespace Totostore.Backend.Application.Catalog.Products;

public class ProductsByBrandSpec : Specification<Product>
{
    public ProductsByBrandSpec(Guid supplierId) =>
        Query.Where(p => p.SupplierId == supplierId);
}