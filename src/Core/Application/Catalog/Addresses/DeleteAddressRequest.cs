using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Addresses;

public class DeleteAddressRequest : IRequest<Guid>
{
    public DeleteAddressRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteAddressRequestHandler : IRequestHandler<DeleteAddressRequest, Guid>
{
    private readonly IStringLocalizer<DeleteAddressRequestHandler> _localizer;
    private readonly IRepository<Address> _repository;

    public DeleteAddressRequestHandler(IRepository<Address> repository,
        IStringLocalizer<DeleteAddressRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = address ?? throw new NotFoundException(_localizer["address.notfound"]);

        // Add Domain Events to be raised after the commit
        address.DomainEvents.Add(EntityDeletedEvent.WithEntity(address));

        await _repository.DeleteAsync(address, cancellationToken);

        return request.Id;
    }
}