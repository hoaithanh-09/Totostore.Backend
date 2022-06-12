using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Details;

public class DeleteDetailRequest : IRequest<Guid>
{
    public DeleteDetailRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteDetailRequestHandler : IRequestHandler<DeleteDetailRequest, Guid>
{
    private readonly IStringLocalizer<DeleteDetailRequestHandler> _localizer;
    private readonly IRepository<Detail> _repository;

    public DeleteDetailRequestHandler(IRepository<Detail> repository,
        IStringLocalizer<DeleteDetailRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteDetailRequest request, CancellationToken cancellationToken)
    {
        var detail = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = detail ?? throw new NotFoundException(_localizer["detail.notfound"]);

        // Add Domain Events to be raised after the commit
        detail.DomainEvents.Add(EntityDeletedEvent.WithEntity(detail));

        await _repository.DeleteAsync(detail, cancellationToken);

        return request.Id;
    }
}