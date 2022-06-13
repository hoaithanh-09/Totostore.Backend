namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class GetProductPriceRequest : IRequest<ProductPriceDetailsDto>
{
    public GetProductPriceRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class ProductPriceByIdSpec : Specification<ProductPrice, ProductPriceDto>, ISingleResultSpecification
{
    public ProductPriceByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Product);
}

public class GetProductPriceRequestHandler : IRequestHandler<GetProductPriceRequest, ProductPriceDetailsDto>
{
    private readonly IStringLocalizer<GetProductPriceRequestHandler> _localizer;
    private readonly IRepository<ProductPrice> _repository;

    public GetProductPriceRequestHandler(IRepository<ProductPrice> repository,
        IStringLocalizer<GetProductPriceRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<ProductPriceDetailsDto> Handle(GetProductPriceRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<ProductPrice, ProductPriceDetailsDto>)new ProductPriceByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["productPrice.notfound"], request.Id));
}