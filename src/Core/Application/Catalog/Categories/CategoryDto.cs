namespace Totostore.Backend.Application.Catalog.Categories;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
    public string? Description { get; set; }
    public int ParentId { get; set; }
    public bool IsShowed { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}