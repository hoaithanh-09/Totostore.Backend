namespace Totostore.Backend.Application.Catalog.Categories;

public class CategoryByNameSpec : Specification<CategoryDetailDto>, ISingleResultSpecification
{
    public CategoryByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}