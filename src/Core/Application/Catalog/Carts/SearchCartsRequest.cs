namespace Totostore.Backend.Application.Catalog.Carts;

public class SearchCartsRequest : PaginationFilter, IRequest<PaginationResponse<CartDto>>
{
    public string? UserId { get; set; }
}

public class SearchCartsRequestHandler : IRequestHandler<SearchCartsRequest, PaginationResponse<CartDto>>
{
    private readonly IReadRepository<Cart> _repository;

    public SearchCartsRequestHandler(IReadRepository<Cart> repository) => _repository = repository;

    public async Task<PaginationResponse<CartDto>> Handle(SearchCartsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new CartsBySearchRequestWithUsersSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize,
            cancellationToken: cancellationToken);
    }
}