namespace Totostore.Backend.Application.Catalog.Categories;

public class CategoryDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public int Level { get; set; }
    public int Order { get; set; }
    public string? Description { get; set; }
    public int ParentId { get; set; }
    public bool IsShowed { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}