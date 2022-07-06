using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class CreateCategoryProductRequest : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
}

public class CreateCategoryProductRequestValidator : CustomValidator<CreateCategoryProductRequest>
{
    public CreateCategoryProductRequestValidator(IReadRepository<CategoryProduct> categoryProductRepo,
        IReadRepository<Category> categoryRepo, IReadRepository<Product> productRepo,
        IStringLocalizer<CreateCategoryProductRequestValidator> localizer)
    {
        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await categoryRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["category.notfound"], id));
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await productRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["product.notfound"], id));
    }
}

public class CreateCategoryProductRequestHandler : IRequestHandler<CreateCategoryProductRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<CategoryProduct> _repository;

    public CreateCategoryProductRequestHandler(IRepository<CategoryProduct> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateCategoryProductRequest request, CancellationToken cancellationToken)
    {
        var categoryProduct = new CategoryProduct(request.ProductId, request.CategoryId);

        // Add Domain Events to be raised after the commit
        categoryProduct.DomainEvents.Add(EntityCreatedEvent.WithEntity(categoryProduct));

        await _repository.AddAsync(categoryProduct, cancellationToken);

        return categoryProduct.Id;
    }
}