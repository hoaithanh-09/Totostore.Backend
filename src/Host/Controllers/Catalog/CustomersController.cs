using Totostore.Backend.Application.Catalog.Customers;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class CustomersController : VersionedApiController
{
    [HttpPost("search")]
    [Authorize]
    //[MustHavePermission(FSHAction.Search, FSHResource.Customers)]
    [OpenApiOperation("Search customers using available filters.", "")]
    public Task<PaginationResponse<CustomerDetailsDto>> SearchAsync(SearchCustomersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    //  [MustHavePermission(FSHAction.View, FSHResource.Customers)]
    [OpenApiOperation("Get customer details.", "")]
    public Task<CustomerDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCustomerRequest(id));
    }

    [HttpPost]
    [TenantIdHeader]
    [AllowAnonymous]
    // [MustHavePermission(FSHAction.Create, FSHResource.Customers)]
    [OpenApiOperation("Create a new customer.", "")]
    public Task<Guid> CreateAsync(CreateCustomerRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    //[MustHavePermission(FSHAction.Update, FSHResource.Customers)]
    [OpenApiOperation("Update a customer.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCustomerRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    //  [MustHavePermission(FSHAction.Delete, FSHResource.Customers)]
    [OpenApiOperation("Delete a customer.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCustomerRequest(id));
    }
}