using MediatR;

namespace Totostore.Backend.Host.Controllers;

[ApiController]
public class BaseApiController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}