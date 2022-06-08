namespace Totostore.Backend.Application.Catalog.Products;

public class ProductByNameSpec : Specification<Product>, ISingleResultSpecification
{
    public ProductByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}