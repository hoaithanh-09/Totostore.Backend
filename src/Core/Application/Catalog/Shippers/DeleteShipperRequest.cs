using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Shippers;

public class DeleteShipperRequest : IRequest<Guid>
{
    public DeleteShipperRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteShipperRequestHandler : IRequestHandler<DeleteShipperRequest, Guid>
{
    private readonly IStringLocalizer<DeleteShipperRequestHandler> _localizer;
    private readonly IRepository<Shipper> _repository;

    public DeleteShipperRequestHandler(IRepository<Shipper> repository,
        IStringLocalizer<DeleteShipperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteShipperRequest request, CancellationToken cancellationToken)
    {
        var shipper = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = shipper ?? throw new NotFoundException(_localizer["shipper.notfound"]);

        // Add Domain Events to be raised after the commit
        shipper.DomainEvents.Add(EntityDeletedEvent.WithEntity(shipper));

        await _repository.DeleteAsync(shipper, cancellationToken);

        return request.Id;
    }
}