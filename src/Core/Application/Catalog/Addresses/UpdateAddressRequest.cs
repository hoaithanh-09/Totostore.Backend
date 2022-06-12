namespace Totostore.Backend.Application.Catalog.Addresses;

public class UpdateAddressRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Ward { get; set; } = default!;
    public string StayingAddress { get; set; } = default!;
}

public class UpdateAddressRequestValidator : CustomValidator<UpdateAddressRequest>
{
    public UpdateAddressRequestValidator(IReadRepository<Address> addressRepo,
        IStringLocalizer<UpdateAddressRequestValidator> localizer)
    {
    }
}

public class UpdateAddressRequestHandler : IRequestHandler<UpdateAddressRequest, Guid>
{
    private readonly IStringLocalizer<UpdateAddressRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Address> _repository;

    public UpdateAddressRequestHandler(IRepositoryWithEvents<Address> repository,
        IStringLocalizer<UpdateAddressRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = address ?? throw new NotFoundException(string.Format(_localizer["address.notfound"], request.Id));

        address.Update(request.City, request.District, request.Ward, request.StayingAddress);

        await _repository.UpdateAsync(address, cancellationToken);

        return request.Id;
    }
}