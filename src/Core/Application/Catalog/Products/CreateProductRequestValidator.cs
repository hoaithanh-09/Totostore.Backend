namespace Totostore.Backend.Application.Catalog.Products;

public class CreateProductRequestValidator : CustomValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IReadRepository<Product> productRepo, IReadRepository<Supplier> supplierRepo,
        IStringLocalizer<CreateProductRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await productRepo.GetBySpecAsync(new ProductByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["product.alreadyexists"], name));

        RuleFor(p => p.Rate)
            .GreaterThanOrEqualTo(1);

        RuleFor(p => p.SupplierId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await supplierRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["supplier.notfound"], id));
    }
}