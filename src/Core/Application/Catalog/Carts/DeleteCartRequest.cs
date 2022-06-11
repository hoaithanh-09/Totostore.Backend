using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Carts;

public class DeleteCartRequest : IRequest<Guid>
{
    public DeleteCartRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteCartRequestHandler : IRequestHandler<DeleteCartRequest, Guid>
{
    private readonly IStringLocalizer<DeleteCartRequestHandler> _localizer;
    private readonly IRepository<Cart> _repository;

    public DeleteCartRequestHandler(IRepository<Cart> repository,
        IStringLocalizer<DeleteCartRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteCartRequest request, CancellationToken cancellationToken)
    {
        var cart = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = cart ?? throw new NotFoundException(_localizer["cart.notfound"]);

        // Add Domain Events to be raised after the commit
        cart.DomainEvents.Add(EntityDeletedEvent.WithEntity(cart));

        await _repository.DeleteAsync(cart, cancellationToken);

        return request.Id;
    }
}