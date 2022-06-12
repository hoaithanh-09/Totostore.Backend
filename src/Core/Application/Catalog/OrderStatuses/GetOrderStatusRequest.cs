namespace Totostore.Backend.Application.Catalog.OrderStatuses;

public class GetOrderStatusRequest : IRequest<OrderStatusDto>
{
    public GetOrderStatusRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class OrderStatusByIdSpec : Specification<OrderStatus, OrderStatusDto>, ISingleResultSpecification
{
    public OrderStatusByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Order);
}

public class GetOrderStatusRequestHandler : IRequestHandler<GetOrderStatusRequest, OrderStatusDto>
{
    private readonly IStringLocalizer<GetOrderStatusRequestHandler> _localizer;
    private readonly IRepository<OrderStatus> _repository;

    public GetOrderStatusRequestHandler(IRepository<OrderStatus> repository,
        IStringLocalizer<GetOrderStatusRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<OrderStatusDto> Handle(GetOrderStatusRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<OrderStatus, OrderStatusDto>)new OrderStatusByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["orderStatus.notfound"], request.Id));
}