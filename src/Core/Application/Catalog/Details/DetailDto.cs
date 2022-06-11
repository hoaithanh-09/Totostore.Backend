namespace Totostore.Backend.Application.Catalog.Details;

public class DetailDto : IDto
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public Guid SupplierId { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
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