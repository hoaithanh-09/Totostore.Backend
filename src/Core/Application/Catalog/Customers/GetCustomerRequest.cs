namespace Totostore.Backend.Application.Catalog.Customers;

public class GetCustomerRequest : IRequest<CustomerDto>
{
    public GetCustomerRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class CustomerByIdSpec : Specification<Customer, CustomerDto>, ISingleResultSpecification
{
    public CustomerByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, CustomerDto>
{
    private readonly IStringLocalizer<GetCustomerRequestHandler> _localizer;
    private readonly IRepository<Customer> _repository;

    public GetCustomerRequestHandler(IRepository<Customer> repository,
        IStringLocalizer<GetCustomerRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<CustomerDto> Handle(GetCustomerRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Customer, CustomerDto>)new CustomerByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["customer.notfound"], request.Id));
}