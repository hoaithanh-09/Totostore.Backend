namespace Totostore.Backend.Application.Catalog.Details;

public class GetDetailRequest: IRequest<DetailDto>
{
    public GetDetailRequest(Guid id) => Id = id;
    public Guid Id { get; set; }

}

public class DetailByIdSpec : Specification<Detail, DetailDto>, ISingleResultSpecification
{
    public DetailByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetDetailRequestHandler : IRequestHandler<GetDetailRequest, DetailDto>
{
    private readonly IStringLocalizer<GetDetailRequestHandler> _localizer;
    private readonly IRepository<Detail> _repository;

    public GetDetailRequestHandler(IRepository<Detail> repository,
        IStringLocalizer<GetDetailRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DetailDto> Handle(GetDetailRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Detail, DetailDto>)new DetailByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["detail.notfound"], request.Id));
}