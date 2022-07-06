namespace Totostore.Backend.Application.Catalog.Customers;

public class SearchCustomersRequest : PaginationFilter, IRequest<PaginationResponse<CustomerDetailsDto>>
{
    public string? UserId { get; set; }
}

public class CustomersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Customer, CustomerDetailsDto>
{
    public CustomersBySearchRequestSpec(SearchCustomersRequest request)
        : base(request)
    {
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
        if (request.UserId != null)
        {
            Query.Where(x => x.UserId == request.UserId);
        }
    }
}

public class SearchCustomersRequestHandler : IRequestHandler<SearchCustomersRequest, PaginationResponse<CustomerDetailsDto>>
{
    private readonly IReadRepository<Customer> _repository;

    public SearchCustomersRequestHandler(IReadRepository<Customer> repository) => _repository = repository;

    public async Task<PaginationResponse<CustomerDetailsDto>> Handle(SearchCustomersRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new CustomersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}