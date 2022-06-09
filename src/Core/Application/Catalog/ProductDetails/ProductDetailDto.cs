namespace Totostore.Backend.Application.Catalog.ProductDetails;

public class ProductDetailDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid DetailId { get; set; }
    public string ProductName { get; set; }
}