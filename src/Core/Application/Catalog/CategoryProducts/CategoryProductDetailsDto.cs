using Totostore.Backend.Application.Catalog.Categories;
using Totostore.Backend.Application.Catalog.Products;

namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class CategoryProductDetailsDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
    public ProductDto Product { get; set; } = default!;
    public CategoryDto Category { get; set; } = default!;
}