namespace Totostore.Backend.Application.Catalog.Products;

public class ProductsBySupplierSpec : Specification<Product>
{
    public ProductsBySupplierSpec(Guid supplierId) =>
        Query.Where(p => p.SupplierId == supplierId);
}