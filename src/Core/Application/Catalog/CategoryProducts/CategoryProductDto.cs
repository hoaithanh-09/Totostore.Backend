namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class CategoryProductDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
    public string ProductName { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
}