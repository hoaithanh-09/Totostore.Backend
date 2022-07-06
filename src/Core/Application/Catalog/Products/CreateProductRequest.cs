using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Products;

public class CreateProductRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public Status Status { get; set; }
    public Guid SupplierId { get; set; }
    public Guid? CouponId { get; set; }
    public decimal Amount { get; set; }
    public Guid CategoryId { get; set; }
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Product> _repository;
    private readonly IRepository<ProductPrice> _productPriceRepository;
    private readonly IRepository<CategoryProduct> _categoryProductRepository;

    public CreateProductRequestHandler(IRepository<Product> repository, IFileStorageService file, IRepository<ProductPrice> productPriceRepository, IRepository<CategoryProduct> categoryProductRepository) =>
        (_repository, _file, _productPriceRepository, _categoryProductRepository) = (repository, file, productPriceRepository, categoryProductRepository);

    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Slug, request.Description, request.Rate, request.Quantity,
            request.Status, request.SupplierId);
        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));

        await _repository.AddAsync(product, cancellationToken);

        var productPrice = new ProductPrice(product.Id, request.CouponId, request.Amount);
        productPrice.DomainEvents.Add(EntityCreatedEvent.WithEntity(productPrice));

        await _productPriceRepository.AddAsync(productPrice, cancellationToken);

        var categoryProduct = new CategoryProduct(product.Id, request.CategoryId);
        categoryProduct.DomainEvents.Add(EntityCreatedEvent.WithEntity(productPrice));

        await _categoryProductRepository.AddAsync(categoryProduct, cancellationToken);

        return product.Id;
    }
}