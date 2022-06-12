namespace Totostore.Backend.Domain.Catalog;

public class Detail : AuditableEntity, IAggregateRoot
{
    public Detail(string? description, string? screenSize, string? itemWeight,
        string? computerMemoryType, string? productDimensions, string? processorBrand,
        string? flashMemorySize, int? processorCount, string? cpuModel, string? cpuModelManufacturer, string? hardDisk,
        string? operatingSystem, string? ramType, string? itemModelNumber, string? color,
        string? series, string? displayResolutionMaximum)
    {
        Description = description;
        ScreenSize = screenSize;
        ItemWeight = itemWeight;
        ComputerMemoryType = computerMemoryType;
        ProductDimensions = productDimensions;
        ProcessorBrand = processorBrand;
        FlashMemorySize = flashMemorySize;
        ProcessorCount = processorCount;
        CpuModel = cpuModel;
        CpuModelManufacturer = cpuModelManufacturer;
        HardDisk = hardDisk;
        OperatingSystem = operatingSystem;
        RamType = ramType;
        ItemModelNumber = itemModelNumber;
        Color = color;
        Series = series;
        DisplayResolutionMaximum = displayResolutionMaximum;
    }

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
    public virtual List<ProductDetail> ProductDetails { get; set; } = default!;

    public Detail Update(string? description, string? screenSize, string? itemWeight,
        string? computerMemoryType, string? productDimensions, string? processorBrand,
        string? flashMemorySize, int? processorCount, string? cpuModel, string? cpuModelManufacturer, string? hardDisk,
        string? operatingSystem, string? ramType, string? itemModelNumber, string? color,
        string? series, string? displayResolutionMaximum)
    {
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (screenSize is not null && ScreenSize?.Equals(screenSize) is not true) ScreenSize = screenSize;
        if (itemWeight is not null && ItemWeight?.Equals(itemWeight) is not true) ItemWeight = itemWeight;
        if (computerMemoryType is not null && ComputerMemoryType?.Equals(computerMemoryType) is not true)
            ComputerMemoryType = computerMemoryType;
        if (productDimensions is not null && ProductDimensions?.Equals(productDimensions) is not true)
            ProductDimensions = productDimensions;
        if (processorBrand is not null && ProcessorBrand?.Equals(processorBrand) is not true)
            ProcessorBrand = processorBrand;
        if (flashMemorySize is not null && FlashMemorySize?.Equals(flashMemorySize) is not true)
            FlashMemorySize = flashMemorySize;
        if (processorCount.HasValue && ProcessorCount != processorCount) ProcessorCount = processorCount.Value;
        if (cpuModel is not null && CpuModel?.Equals(cpuModel) is not true) CpuModel = cpuModel;
        if (cpuModelManufacturer is not null && CpuModelManufacturer?.Equals(cpuModelManufacturer) is not true)
            CpuModelManufacturer = cpuModelManufacturer;
        if (hardDisk is not null && HardDisk?.Equals(hardDisk) is not true) HardDisk = hardDisk;
        if (operatingSystem is not null && OperatingSystem?.Equals(operatingSystem) is not true)
            OperatingSystem = operatingSystem;
        if (ramType is not null && RamType?.Equals(ramType) is not true) RamType = ramType;
        if (itemModelNumber is not null && ItemModelNumber?.Equals(itemModelNumber) is not true)
            ItemModelNumber = itemModelNumber;
        if (color is not null && Color?.Equals(color) is not true) Color = color;
        if (series is not null && Series?.Equals(series) is not true) Series = series;
        if (displayResolutionMaximum is not null &&
            DisplayResolutionMaximum?.Equals(displayResolutionMaximum) is not true)
            DisplayResolutionMaximum = displayResolutionMaximum;
        return this;
    }
}