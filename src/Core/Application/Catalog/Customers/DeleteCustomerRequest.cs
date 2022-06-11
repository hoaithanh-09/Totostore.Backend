using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Customers;

public class DeleteCustomerRequest : IRequest<Guid>
{
    public DeleteCustomerRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, Guid>
{
    private readonly IStringLocalizer<DeleteCustomerRequestHandler> _localizer;
    private readonly IRepository<Customer> _repository;

    public DeleteCustomerRequestHandler(IRepository<Customer> repository,
        IStringLocalizer<DeleteCustomerRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = customer ?? throw new NotFoundException(_localizer["category.notfound"]);

        // Add Domain Events to be raised after the commit
        customer.DomainEvents.Add(EntityDeletedEvent.WithEntity(customer));

        await _repository.DeleteAsync(customer, cancellationToken);

        return request.Id;
    }
}