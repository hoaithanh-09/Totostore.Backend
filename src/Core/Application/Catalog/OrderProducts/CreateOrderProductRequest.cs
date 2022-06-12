using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.OrderProducts;

public class CreateOrderProductRequest : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductPriceId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderProductRequestValidator : CustomValidator<CreateOrderProductRequest>
{
    public CreateOrderProductRequestValidator(IReadRepository<OrderProduct> orderProductRepo,
        IReadRepository<Order> orderRepo, IReadRepository<Product> productRepo,
        IReadRepository<ProductPrice> productPriceRepo, IStringLocalizer<CreateOrderProductRequestValidator> localizer)
    {
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await orderRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["order.notfound"], id));
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
        RuleFor(p => p.ProductPriceId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productPriceRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["productPrice.notfound"], id));
    }
}

public class CreateOrderProductRequestHandler : IRequestHandler<CreateOrderProductRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<OrderProduct> _repository;

    public CreateOrderProductRequestHandler(IRepository<OrderProduct> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateOrderProductRequest request, CancellationToken cancellationToken)
    {
        var orderProduct =
            new OrderProduct(request.ProductId, request.OrderId, request.ProductPriceId, request.Quantity);

        // Add Domain Events to be raised after the commit
        orderProduct.DomainEvents.Add(EntityCreatedEvent.WithEntity(orderProduct));

        await _repository.AddAsync(orderProduct, cancellationToken);

        return orderProduct.Id;
    }
}