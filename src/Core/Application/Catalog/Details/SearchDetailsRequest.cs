namespace Totostore.Backend.Application.Catalog.Details;

public class SearchDetailsRequest: PaginationFilter, IRequest<PaginationResponse<DetailDto>>
{
}

public class DetailsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Detail, DetailDto>
{
    public DetailsBySearchRequestSpec(SearchDetailsRequest request)
        : base(request) =>
        Query.OrderBy(x => x.CreatedOn);
}

public class SearchDetailsRequestHandler : IRequestHandler<SearchDetailsRequest, PaginationResponse<DetailDto>>
{
    private readonly IReadRepository<Detail> _repository;

    public SearchDetailsRequestHandler(IReadRepository<Detail> repository) => _repository = repository;

    public async Task<PaginationResponse<DetailDto>> Handle(SearchDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new DetailsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}