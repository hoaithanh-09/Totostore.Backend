using MapsterMapper;
using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class CreateProductPriceRequest : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public Guid? CouponId { get; set; }
    public decimal Amount { get; set; }
}

public class CreateProductPriceRequestValidator : CustomValidator<CreateProductPriceRequest>
{
    public CreateProductPriceRequestValidator(IReadRepository<ProductPrice> productPriceRepo, IReadRepository<Product> productRepo, IStringLocalizer<CreateProductPriceRequestValidator> localizer)
    {
        RuleFor(p => p.ProductId)
           .NotEmpty()
           .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
           .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
    }
}

public class CreateProductPriceRequestHandler : IRequestHandler<CreateProductPriceRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<ProductPrice> _repository;

    public CreateProductPriceRequestHandler(IRepository<ProductPrice> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateProductPriceRequest request, CancellationToken cancellationToken)
    {
        var productPrice = new ProductPrice(request.ProductId, request.CouponId, request.Amount);
        // Add Domain Events to be raised after the commit
        productPrice.DomainEvents.Add(EntityCreatedEvent.WithEntity(productPrice));

        await _repository.AddAsync(productPrice, cancellationToken);

        return productPrice.Id;
    }
}