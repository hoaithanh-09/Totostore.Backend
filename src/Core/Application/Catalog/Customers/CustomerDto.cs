namespace Totostore.Backend.Application.Catalog.Customers;

public class CustomerDto: IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DOB { get; set; }
    public bool Gender { get; set; }
    public string Mail { get; set; }
    public string PhoneNumber { get; set; }
    public Guid AddressId { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}