namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class CategoryProductDetailsDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
}