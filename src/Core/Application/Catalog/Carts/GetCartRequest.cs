namespace Totostore.Backend.Application.Catalog.Carts;

public class GetCartRequest : IRequest<CartDetailsDto>
{
    public GetCartRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class CartByIdSpec : Specification<Cart, CartDetailsDto>, ISingleResultSpecification
{
    public CartByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.UserId)
            .Include(p => p.Product);
}

public class GetCartRequestHandler : IRequestHandler<GetCartRequest, CartDetailsDto>
{
    private readonly IStringLocalizer<GetCartRequestHandler> _localizer;
    private readonly IRepository<Cart> _repository;

    public GetCartRequestHandler(IRepository<Cart> repository, IStringLocalizer<GetCartRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<CartDetailsDto> Handle(GetCartRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Cart, CartDetailsDto>)new CartByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["cart.notfound"], request.Id));
}