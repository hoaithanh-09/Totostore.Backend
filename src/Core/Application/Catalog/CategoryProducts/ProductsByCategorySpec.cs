namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class ProductsByCategorySpec : Specification<CategoryProduct>
{
    public ProductsByCategorySpec(Guid CategotyId) =>
        Query.Where(p => p.CategoryId == CategotyId);
}