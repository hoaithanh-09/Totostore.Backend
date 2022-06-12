using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Details;

public class UpdateDetailRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? ScreenSize { get; set; }
    public string? ItemWeight { get; set; }
    public string? ComputerMemoryType { get; set; }
    public string? ProductDimensions { get; set; } // kich thuoc sp
    public string? ProcessorBrand { get; set; }
    public string? FlashMemorySize { get; set; } // kich thuoc bo nho flash
    public int? ProcessorCount { get; set; }
    public string? CpuModel { get; set; }
    public string? CpuModelManufacturer { get; set; }
    public string? HardDisk { get; set; } // type+size dish
    public string? OperatingSystem { get; set; }
    public string? RamType { get; set; }
    public string? ItemModelNumber { get; set; }
    public string? Color { get; set; }
    public string? Series { get; set; }
    public string? DisplayResolutionMaximum { get; set; }
}

public class UpdateDetailRequestValidator : CustomValidator<UpdateDetailRequest>
{
    public UpdateDetailRequestValidator(IReadRepository<Detail> repository,
        IStringLocalizer<UpdateDetailRequestValidator> localizer)
    {
    }
}

public class UpdateDetailRequestHandler : IRequestHandler<UpdateDetailRequest, Guid>
{
    private readonly IStringLocalizer<UpdateDetailRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Detail> _repository;

    public UpdateDetailRequestHandler(IRepositoryWithEvents<Detail> repository,
        IStringLocalizer<UpdateDetailRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateDetailRequest request, CancellationToken cancellationToken)
    {
        var detail = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = detail ?? throw new NotFoundException(string.Format(_localizer["detail.notfound"], request.Id));

        detail.Update(request.Description, request.ScreenSize, request.ItemWeight, request.ComputerMemoryType,
            request.ProductDimensions,
            request.ProcessorBrand, request.FlashMemorySize, request.ProcessorCount, request.CpuModel,
            request.CpuModelManufacturer, request.HardDisk,
            request.OperatingSystem, request.RamType, request.ItemModelNumber, request.Color, request.Series,
            request.DisplayResolutionMaximum);
        // Add Domain Events to be raised after the commit
        detail.DomainEvents.Add(EntityUpdatedEvent.WithEntity(detail));

        await _repository.UpdateAsync(detail, cancellationToken);

        return request.Id;
    }
}