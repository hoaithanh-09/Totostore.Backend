namespace Totostore.Backend.Application.Catalog.OrderProducts;

public class GetOrderProductRequest : IRequest<OrderProductDto>
{
    public GetOrderProductRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class OrderProductByIdSpec : Specification<OrderProduct, OrderProductDto>, ISingleResultSpecification
{
    public OrderProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Order)
            .Include(p => p.Product)
            .Include(p => p.ProductPrice);
}

public class GetOrderProductRequestHandler : IRequestHandler<GetOrderProductRequest, OrderProductDto>
{
    private readonly IStringLocalizer<GetOrderProductRequestHandler> _localizer;
    private readonly IRepository<OrderProduct> _repository;

    public GetOrderProductRequestHandler(IRepository<OrderProduct> repository,
        IStringLocalizer<GetOrderProductRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<OrderProductDto> Handle(GetOrderProductRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<OrderProduct, OrderProductDto>)new OrderProductByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["orderProduct.notfound"], request.Id));
}