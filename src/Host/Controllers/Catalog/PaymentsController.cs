using Totostore.Backend.Application.Catalog.Payments;

namespace Totostore.Backend.Host.Controllers.Catalog;

public class PaymentsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Payments)]
    [OpenApiOperation("Search payments using available filters.", "")]
    public Task<PaginationResponse<PaymentDto>> SearchAsync(SearchPaymentsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Payments)]
    [OpenApiOperation("Get payment details.", "")]
    public Task<PaymentDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetPaymentRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Payments)]
    [OpenApiOperation("Create a new payment.", "")]
    public Task<Guid> CreateAsync(CreatePaymentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Payments)]
    [OpenApiOperation("Update a payment.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdatePaymentRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Payments)]
    [OpenApiOperation("Delete a payment.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeletePaymentRequest(id));
    }
}