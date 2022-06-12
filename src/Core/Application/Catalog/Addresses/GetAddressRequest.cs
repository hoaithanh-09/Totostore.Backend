namespace Totostore.Backend.Application.Catalog.Addresses;

public class GetAddressRequest : IRequest<AddressDto>
{
    public GetAddressRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class AddressByIdSpec : Specification<Address, AddressDto>, ISingleResultSpecification
{
    public AddressByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetAddressRequestHandler : IRequestHandler<GetAddressRequest, AddressDto>
{
    private readonly IStringLocalizer<GetAddressRequestHandler> _localizer;
    private readonly IRepository<Address> _repository;

    public GetAddressRequestHandler(IRepository<Address> repository,
        IStringLocalizer<GetAddressRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AddressDto> Handle(GetAddressRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Address, AddressDto>)new AddressByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["address.notfound"], request.Id));
}