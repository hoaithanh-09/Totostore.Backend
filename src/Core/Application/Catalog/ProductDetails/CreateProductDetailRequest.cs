using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.ProductDetails;

public class CreateProductDetailRequest : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public Guid DetailId { get; set; }
}

public class CreateOrderDetailRequestValidator : CustomValidator<CreateProductDetailRequest>
{
    public CreateOrderDetailRequestValidator(IReadRepository<ProductDetail> productDetailRepo,
        IReadRepository<Product> productRepo, IReadRepository<Detail> detailtRepo,
        IStringLocalizer<CreateOrderDetailRequestValidator> localizer)
    {
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
        RuleFor(p => p.DetailId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await detailtRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["detail.notfound"], id));
    }
}

public class CreateProductDetailRequestHandler : IRequestHandler<CreateProductDetailRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<ProductDetail> _repository;

    public CreateProductDetailRequestHandler(IRepository<ProductDetail> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateProductDetailRequest request, CancellationToken cancellationToken)
    {
        var productDetail =
            new ProductDetail(request.ProductId, request.DetailId);

        // Add Domain Events to be raised after the commit
        productDetail.DomainEvents.Add(EntityCreatedEvent.WithEntity(productDetail));

        await _repository.AddAsync(productDetail, cancellationToken);

        return productDetail.Id;
    }
}