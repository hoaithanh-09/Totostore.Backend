namespace Totostore.Backend.Application.Catalog.ProductImages;

public class GetProductImageRequest : IRequest<ProductImageDto>
{
    public GetProductImageRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class ProductImageByIdSpec : Specification<ProductImage, ProductImageDto>, ISingleResultSpecification
{
    public ProductImageByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Product);
}

public class GetProductImageRequestHandler : IRequestHandler<GetProductImageRequest, ProductImageDto>
{
    private readonly IStringLocalizer<GetProductImageRequestHandler> _localizer;
    private readonly IRepository<ProductImage> _repository;

    public GetProductImageRequestHandler(IRepository<ProductImage> repository,
        IStringLocalizer<GetProductImageRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ProductImageDto> Handle(GetProductImageRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<ProductImage, ProductImageDto>)new ProductImageByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["productImage.notfound"], request.Id));
}