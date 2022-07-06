namespace Totostore.Backend.Application.Catalog.Carts;

public class CartsBySearchRequestWithUsersSpec : EntitiesByPaginationFilterSpec<Cart, CartDto>
{
    public CartsBySearchRequestWithUsersSpec(SearchCartsRequest request)
        : base(request)
        {
          Query.OrderBy(x => x.CreatedOn);
        if(request.UserId!=null)
        {
            Query.Where(x=>x.UserId.Contains(request.UserId));
        }

        }
      
}