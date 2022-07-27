using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Domain.Catalog;

public class ProductImage : AuditableEntity, IAggregateRoot
{
    public ProductImage(Guid productId, string[] imagePath, long? fileSize, ProductImageType type, string? description)
    {
        ProductId = productId;
        ImagePath = imagePath;
        FileSize = fileSize;
        Type = type;
        Description = description;
    }

    public Guid ProductId { get; set; }
    public string[] ImagePath { get; set; } = default!;
    public long? FileSize { get; set; }
    public ProductImageType Type { get; set; }
    public string? Description { get; set; }

    public virtual Product Product { get; set; } = default!;

    public ProductImage Update(Guid? productId, string[]? imagePath, long? fileSize, ProductImageType? type,
        string? description)
    {
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value))
            ProductId = productId.Value;
        if (fileSize.HasValue && FileSize != fileSize) FileSize = fileSize.Value;
       // if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        if (type.HasValue && Type != type) Type = type.Value;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}