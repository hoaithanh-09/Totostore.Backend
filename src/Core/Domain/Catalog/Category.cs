namespace Totostore.Backend.Domain.Catalog;

public class CategoryDetailDto : AuditableEntity, IAggregateRoot
{
    public CategoryDetailDto(string name, string? slug, int level, int order, string? description, Guid? parentId, bool isShowed)
    {
        Name = name;
        Slug = slug;
        Level = level;
        Order = order;
        Description = description;
        ParentId = parentId;
        IsShowed = isShowed;
    }

    public string Name { get; set; } = default!;
    public string? Slug { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsShowed { get; set; }
    public virtual List<CategoryProduct> CategoryProducts { get; set; } = default!;

    public CategoryDetailDto Update(string? name, string? slug, int? level, int? order, string? description, Guid? parentId,
        bool? isShowed)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (slug is not null && Slug?.Equals(slug) is not true) Slug = slug;
        if (level.HasValue && Level != level) Level = level.Value;
        if (order.HasValue && Order != order) Order = order.Value;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (parentId.HasValue && parentId.Value != Guid.Empty && !ParentId.Equals(parentId.Value))
            ParentId = parentId.Value;
        if (isShowed.HasValue && IsShowed != isShowed) IsShowed = isShowed.Value;
        return this;
    }
}