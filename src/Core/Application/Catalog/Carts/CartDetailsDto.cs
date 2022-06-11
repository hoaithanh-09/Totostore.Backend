using Totostore.Backend.Application.Catalog.Customers;
using Totostore.Backend.Application.Catalog.Products;

namespace Totostore.Backend.Application.Catalog.Carts;

public class CartDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public ProductDto Product { get; set; } = default!;
    public CustomerDto Customer { get; set; } = default!;
}