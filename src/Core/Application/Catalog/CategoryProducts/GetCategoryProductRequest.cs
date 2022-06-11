namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class GetCategoryProductRequest : IRequest<CategoryProductDetailsDto>
{
    public GetCategoryProductRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class CategoryProductByIdSpec : Specification<CategoryProduct, CategoryProductDetailsDto>,
    ISingleResultSpecification
{
    public CategoryProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Category)
            .Include(p => p.Product);
}

public class GetCategoryProductRequestHandler : IRequestHandler<GetCategoryProductRequest, CategoryProductDetailsDto>
{
    private readonly IStringLocalizer<GetCategoryProductRequestHandler> _localizer;
    private readonly IRepository<CategoryProduct> _repository;

    public GetCategoryProductRequestHandler(IRepository<CategoryProduct> repository,
        IStringLocalizer<GetCategoryProductRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<CategoryProductDetailsDto> Handle(GetCategoryProductRequest request,
        CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<CategoryProduct, CategoryProductDetailsDto>)new CategoryProductByIdSpec(request.Id),
            cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["categoryProduct.notfound"], request.Id));
}