namespace Totostore.Backend.Application.Catalog.Details;

public class DetailDto:IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}