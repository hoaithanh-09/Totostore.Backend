using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class UpdateProductPriceRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
    public Guid? CouponId { get; set; }
}

public class UpdateProductPriceRequestValidator : CustomValidator<UpdateProductPriceRequest>
{
    public UpdateProductPriceRequestValidator(IReadRepository<ProductPrice> productPriceRepo,
        IReadRepository<Product> productRepo, IStringLocalizer<UpdateProductPriceRequestValidator> localizer)
    {
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
    }
}

public class UpdateProductPriceRequestHandler : IRequestHandler<UpdateProductPriceRequest, Guid>
{
    private readonly IStringLocalizer<UpdateProductPriceRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ProductPrice> _repository;

    public UpdateProductPriceRequestHandler(IRepositoryWithEvents<ProductPrice> repository,
        IStringLocalizer<UpdateProductPriceRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateProductPriceRequest request, CancellationToken cancellationToken)
    {
        var productPrice = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = productPrice ?? throw new NotFoundException(string.Format(_localizer["productPrice.notfound"], request.Id));

        productPrice.Update(request.ProductId, request.CouponId, request.Amount);
        // Add Domain Events to be raised after the commit
        productPrice.DomainEvents.Add(EntityUpdatedEvent.WithEntity(productPrice));

        await _repository.UpdateAsync(productPrice, cancellationToken);

        return request.Id;
    }
}
