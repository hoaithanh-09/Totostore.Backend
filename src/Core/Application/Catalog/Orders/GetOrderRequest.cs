namespace Totostore.Backend.Application.Catalog.Orders;

public class GetOrderRequest : IRequest<OrderDetailsDto>
{
    public GetOrderRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class OrderByIdSpec : Specification<Order, OrderDetailsDto>, ISingleResultSpecification
{
    public OrderByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Customer)
            .Include(p => p.AddressDelivery);
}

public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDetailsDto>
{
    private readonly IStringLocalizer<GetOrderRequestHandler> _localizer;
    private readonly IRepository<Order> _repository;

    public GetOrderRequestHandler(IRepository<Order> repository, IStringLocalizer<GetOrderRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<OrderDetailsDto> Handle(GetOrderRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Order, OrderDetailsDto>)new OrderByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["order.notfound"], request.Id));
}