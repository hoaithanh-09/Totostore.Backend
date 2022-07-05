using Totostore.Backend.Application.Catalog.Addresses;
using Totostore.Backend.Application.Identity.Users;

namespace Totostore.Backend.Application.Catalog.Customers;

public class CustomerDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid UserId { get; set; }
    public UserDetailsDto User { get; set; }
    public Guid AddressId { get; set; }
    public AddressDto Address { get; set; } = default!;
}