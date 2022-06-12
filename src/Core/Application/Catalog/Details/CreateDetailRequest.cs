namespace Totostore.Backend.Application.Catalog.Details;

public class CreateDetailRequest : IRequest<Guid>
{
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

public class CreateDetailRequestValidator : CustomValidator<CreateDetailRequest>
{
    public CreateDetailRequestValidator(IReadRepository<Detail> repository,
        IStringLocalizer<CreateDetailRequestValidator> localizer)
    {
    }
}

public class CreateDetailRequestHandler : IRequestHandler<CreateDetailRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Detail> _repository;

    public CreateDetailRequestHandler(IRepositoryWithEvents<Detail> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateDetailRequest request, CancellationToken cancellationToken)
    {
        var detail = new Detail(request.Description, request.ScreenSize, request.ItemWeight, request.ComputerMemoryType,
            request.ProductDimensions,
            request.ProcessorBrand, request.FlashMemorySize, request.ProcessorCount, request.CpuModel,
            request.CpuModelManufacturer, request.HardDisk,
            request.OperatingSystem, request.RamType, request.ItemModelNumber, request.Color, request.Series,
            request.DisplayResolutionMaximum);

        await _repository.AddAsync(detail, cancellationToken);

        return detail.Id;
    }
}