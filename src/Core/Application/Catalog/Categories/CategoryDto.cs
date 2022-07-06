namespace Totostore.Backend.Application.Catalog.Categories;

public class CategoryDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Slug { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsShowed { get; set; }
}