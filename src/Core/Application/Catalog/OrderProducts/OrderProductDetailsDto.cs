using Totostore.Backend.Application.Catalog.Orders;
using Totostore.Backend.Application.Catalog.ProductPrices;
using Totostore.Backend.Application.Catalog.Products;

namespace Totostore.Backend.Application.Catalog.OrderProducts;

public class OrderProductDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public ProductDto Product { get; set; } = default!;
    public OrderDto Order { get; set; } = default!;
}