namespace Totostore.Backend.Application.Catalog.Carts;

public class CartsBySearchRequestWithCustomersSpec : EntitiesByPaginationFilterSpec<Cart, CartDto>
{
    public CartsBySearchRequestWithCustomersSpec(SearchCartsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Customer)
            .Where(p => p.CustomerId.Equals(request.CustomerId!.Value), request.CustomerId.HasValue);
}