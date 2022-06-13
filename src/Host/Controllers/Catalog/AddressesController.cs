using Totostore.Backend.Application.Catalog.Addresses;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class AddressesController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Addresses)]
    [OpenApiOperation("Get address details.", "")]
    public Task<AddressDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAddressRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Addresses)]
    [OpenApiOperation("Create a new address.", "")]
    public Task<Guid> CreateAsync(CreateAddressRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Addresses)]
    [OpenApiOperation("Update a address.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAddressRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Addresses)]
    [OpenApiOperation("Delete a address.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAddressRequest(id));
    }
}