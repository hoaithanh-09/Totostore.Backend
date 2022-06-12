namespace Totostore.Backend.Application.Catalog.Shippers;

public class GetShipperRequest : IRequest<ShipperDetailsDto>
{
    public GetShipperRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class ShipperByIdSpec : Specification<Shipper, ShipperDetailsDto>, ISingleResultSpecification
{
    public ShipperByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetShipperRequestHandler : IRequestHandler<GetShipperRequest, ShipperDetailsDto>
{
    private readonly IStringLocalizer<GetShipperRequestHandler> _localizer;
    private readonly IRepository<Shipper> _repository;

    public GetShipperRequestHandler(IRepository<Shipper> repository,
        IStringLocalizer<GetShipperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ShipperDetailsDto> Handle(GetShipperRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Shipper, ShipperDetailsDto>)new ShipperByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["shipper.notfound"], request.Id));
}