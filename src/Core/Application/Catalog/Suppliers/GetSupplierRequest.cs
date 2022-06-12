namespace Totostore.Backend.Application.Catalog.Suppliers;

public class GetSupplierRequest : IRequest<SupplierDetailsDto>
{
    public GetSupplierRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class SupplierByIdSpec : Specification<Supplier, SupplierDetailsDto>, ISingleResultSpecification
{
    public SupplierByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetSupplierRequestHandler : IRequestHandler<GetSupplierRequest, SupplierDetailsDto>
{
    private readonly IStringLocalizer<GetSupplierRequestHandler> _localizer;
    private readonly IRepository<Supplier> _repository;

    public GetSupplierRequestHandler(IRepository<Supplier> repository,
        IStringLocalizer<GetSupplierRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<SupplierDetailsDto> Handle(GetSupplierRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Supplier, SupplierDetailsDto>)new SupplierByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["supplier.notfound"], request.Id));
}