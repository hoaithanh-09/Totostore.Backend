namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class CategoryProductDto : IDto
{
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
}