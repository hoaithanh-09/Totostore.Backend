namespace Totostore.Backend.Application.Catalog.Customers;

public class SearchCustomersRequest : PaginationFilter, IRequest<PaginationResponse<CustomerDto>>
{
}

public class CustomersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Customer, CustomerDto>
{
    public CustomersBySearchRequestSpec(SearchCustomersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchCategoriesRequestHandler : IRequestHandler<SearchCustomersRequest, PaginationResponse<CustomerDto>>
{
    private readonly IReadRepository<Customer> _repository;

    public SearchCategoriesRequestHandler(IReadRepository<Customer> repository) => _repository = repository;

    public async Task<PaginationResponse<CustomerDto>> Handle(SearchCustomersRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new CustomersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}