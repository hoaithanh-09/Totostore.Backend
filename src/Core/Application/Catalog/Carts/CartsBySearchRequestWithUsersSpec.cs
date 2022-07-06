namespace Totostore.Backend.Application.Catalog.Carts;

public class CartsBySearchRequestWithUsersSpec : EntitiesByPaginationFilterSpec<Cart, CartDto>
{
    public CartsBySearchRequestWithUsersSpec(SearchCartsRequest request)
        : base(request) =>
        Query
            .Include(p => p.UserId)
            .Where(p => p.UserId.Contains(request.UserId!), request.UserId!=null);
}