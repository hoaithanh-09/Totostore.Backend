namespace Totostore.Backend.Application.Catalog.ProductDetails;

public class GetProductDetailRequest : IRequest<ProductDetailDto>
{
    public GetProductDetailRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class ProductDetailByIdSpec : Specification<ProductDetail, ProductDetailDto>, ISingleResultSpecification
{
    public ProductDetailByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Product)
            .Include(p => p.Detail);
}

public class GetProductDetailRequestHandler : IRequestHandler<GetProductDetailRequest, ProductDetailDto>
{
    private readonly IStringLocalizer<GetProductDetailRequestHandler> _localizer;
    private readonly IRepository<ProductDetail> _repository;

    public GetProductDetailRequestHandler(IRepository<ProductDetail> repository,
        IStringLocalizer<GetProductDetailRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ProductDetailDto> Handle(GetProductDetailRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<ProductDetail, ProductDetailDto>)new ProductDetailByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["productDetail.notfound"], request.Id));
}