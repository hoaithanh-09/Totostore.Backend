namespace Totostore.Backend.Application.Catalog.ProductDetails;

public class GetProductDetailRequest : IRequest<ProductDetailDetailsDto>
{
    public GetProductDetailRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class ProductDetailByIdSpec : Specification<ProductDetail, ProductDetailDetailsDto>, ISingleResultSpecification
{
    public ProductDetailByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Product)
            .Include(p => p.Detail);
}

public class GetProductDetailRequestHandler : IRequestHandler<GetProductDetailRequest, ProductDetailDetailsDto>
{
    private readonly IStringLocalizer<GetProductDetailRequestHandler> _localizer;
    private readonly IRepository<ProductDetail> _repository;

    public GetProductDetailRequestHandler(IRepository<ProductDetail> repository,
        IStringLocalizer<GetProductDetailRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ProductDetailDetailsDto> Handle(GetProductDetailRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<ProductDetail, ProductDetailDetailsDto>)new ProductDetailByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["productDetail.notfound"], request.Id));
}