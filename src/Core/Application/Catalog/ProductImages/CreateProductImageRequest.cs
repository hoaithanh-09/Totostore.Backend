using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.ProductImages;

public class CreateProductImageRequest : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public string[] ImagePath { get; set; } = default!;
    public long? FileSize { get; set; }
    public ProductImageType Type { get; set; }
    public string? Description { get; set; }
}

public class CreateProductImageRequestValidator : CustomValidator<CreateProductImageRequest>
{
    public CreateProductImageRequestValidator(IReadRepository<ProductImage> productImageRepo,
        IReadRepository<Product> productRepo, IStringLocalizer<CreateProductImageRequestValidator> localizer)
    {
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
    }
}

public class CreateProductImageRequestHandler : IRequestHandler<CreateProductImageRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<ProductImage> _repository;

    public CreateProductImageRequestHandler(IRepository<ProductImage> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateProductImageRequest request, CancellationToken cancellationToken)
    {
        var productImage =
            new ProductImage(request.ProductId, request.ImagePath, request.FileSize, request.Type, request.Description);

        // Add Domain Events to be raised after the commit
        productImage.DomainEvents.Add(EntityCreatedEvent.WithEntity(productImage));

        await _repository.AddAsync(productImage, cancellationToken);

        return productImage.Id;
    }
}