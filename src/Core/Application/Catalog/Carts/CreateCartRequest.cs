using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Domain.Identity;

namespace Totostore.Backend.Application.Catalog.Carts;

public class CreateCartRequest : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public string UserId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class CreateCartRequestValidator : CustomValidator<CreateCartRequest>
{
    public CreateCartRequestValidator(IReadRepository<Cart> cartRepo,
        IReadRepository<Product> productRepo, IStringLocalizer<CreateCartRequestValidator> localizer)
    { 
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
    }
}

public class CreateCartRequestHandler : IRequestHandler<CreateCartRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Cart> _repository;

    public CreateCartRequestHandler(IRepository<Cart> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateCartRequest request, CancellationToken cancellationToken)
    {
        var cart = new Cart(request.ProductId, request.UserId, request.Quantity, request.Price);

        // Add Domain Events to be raised after the commit
        cart.DomainEvents.Add(EntityCreatedEvent.WithEntity(cart));

        await _repository.AddAsync(cart, cancellationToken);

        return cart.Id;
    }
}