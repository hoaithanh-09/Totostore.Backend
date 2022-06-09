namespace Totostore.Backend.Application.Catalog.Details;

public class DetailDto :IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid SupplierId { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
    public string ScreenSize { get; set; } = default!;
    public string ItemWeight { get; set; } = default!;
    public string ComputerMemoryType { get; set; } = default!;
    public string ProductDimensions { get; set; } = default!; // kich thuoc sp
    public string ProcessorBrand { get; set; } = default!;
    public string FlashMemorySize { get; set; } = default!; // kich thuoc bo nho flash
    public int ProcessorCount { get; set; }
    public string CpuModel { get; set; } = default!;
    public string CpuModelManufacturer { get; set; } = default!;
    public string HardDisk { get; set; } = default!;    // type+size dish
    public string OperatingSystem { get; set; } = default!;
    public string RamType { get; set; } = default!;
    public string ItemModelNumber { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Series { get; set; } = default!;
    public string DisplayResolutionMaximum { get; set; } = default!;
}