namespace Totostore.Backend.Application.Catalog.Products;

public class ProductByIdWSpec : Specification<Product, ProductDetailsDto>, ISingleResultSpecification
{
    public ProductByIdWSpec(Guid id)
    {
        Query
            .Where(p => p.Id == id);
    }
}