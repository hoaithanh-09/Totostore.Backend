namespace Totostore.Backend.Application.Catalog.Addresses;

public class SearchAddressesRequest: PaginationFilter, IRequest<PaginationResponse<AddressDto>>
{
}
public class AddressesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Address, AddressDto>
{
    public AddressesBySearchRequestSpec(SearchAddressesRequest request)
        : base(request) => Query.OrderBy(x => x.CreatedBy);

}

public class SearchAddressesRequestHandler : IRequestHandler<SearchAddressesRequest, PaginationResponse<AddressDto>>
{
    private readonly IReadRepository<Address> _repository;

    public SearchAddressesRequestHandler(IReadRepository<Address> repository) => _repository = repository;

    public async Task<PaginationResponse<AddressDto>> Handle(SearchAddressesRequest request, CancellationToken cancellationToken)
    {
        var spec = new AddressesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}