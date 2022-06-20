namespace Totostore.Backend.Application.Catalog.OrderStatus;

public class GetOrderStatusRequest : IRequest<OrderStatusDetailsDto>
{
    public GetOrderStatusRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class OrderStatusByIdSpec : Specification<Domain.Catalog.OrderStatus, OrderStatusDto>, ISingleResultSpecification
{
    public OrderStatusByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Order);
}

public class GetOrderStatusRequestHandler : IRequestHandler<GetOrderStatusRequest, OrderStatusDetailsDto>
{
    private readonly IStringLocalizer<GetOrderStatusRequestHandler> _localizer;
    private readonly IRepository<Domain.Catalog.OrderStatus> _repository;

    public GetOrderStatusRequestHandler(IRepository<Domain.Catalog.OrderStatus> repository,
        IStringLocalizer<GetOrderStatusRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<OrderStatusDetailsDto> Handle(GetOrderStatusRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Domain.Catalog.OrderStatus, OrderStatusDetailsDto>)new OrderStatusByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["orderStatus.notfound"], request.Id));
}