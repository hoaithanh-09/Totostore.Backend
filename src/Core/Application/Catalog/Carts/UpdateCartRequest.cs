using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Carts;

public class UpdateCartRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class UpdateCartRequestValidator : CustomValidator<UpdateCartRequest>
{
    public UpdateCartRequestValidator(IReadRepository<Cart> cartRepo, IReadRepository<Customer> customerRepo,
        IReadRepository<Product> productRepo, IStringLocalizer<UpdateCartRequestValidator> localizer)
    {
        RuleFor(p => p.CustomerId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await customerRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["customer.notfound"], id));
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await customerRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
    }
}

public class UpdateCartRequestHandler : IRequestHandler<UpdateCartRequest, Guid>
{
    private readonly IStringLocalizer<UpdateCartRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Cart> _repository;

    public UpdateCartRequestHandler(IRepositoryWithEvents<Cart> repository,
        IStringLocalizer<UpdateCartRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCartRequest request, CancellationToken cancellationToken)
    {
        var cart = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = cart ?? throw new NotFoundException(string.Format(_localizer["cart.notfound"], request.Id));

        cart.Update(request.ProductId, request.CustomerId, request.Quantity, request.Price);
        // Add Domain Events to be raised after the commit
        cart.DomainEvents.Add(EntityUpdatedEvent.WithEntity(cart));

        await _repository.UpdateAsync(cart, cancellationToken);

        return request.Id;
    }
}