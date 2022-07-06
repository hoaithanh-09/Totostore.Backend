using Totostore.Backend.Application.Catalog.Customers;
using Totostore.Backend.Application.Catalog.Products;
using Totostore.Backend.Application.Identity.Users;

namespace Totostore.Backend.Application.Catalog.Carts;

public class CartDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string UserId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public ProductDto Product { get; set; } = default!;
    public UserDetailsDto User { get; set; } = default!;
}