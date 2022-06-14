namespace Totostore.Backend.Application.Catalog.Products;

public class ProductByIdWithSupplierSpec : Specification<Product, ProductDetailsDto>, ISingleResultSpecification
{
    public ProductByIdWithSupplierSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Supplier);
}