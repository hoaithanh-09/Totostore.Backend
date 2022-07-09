using Microsoft.AspNetCore.Identity;
using Totostore.Backend.Domain.Catalog;

namespace Totostore.Backend.Domain.Identity;
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? ObjectId { get; set; }
    public virtual List<Customer> Customers { get; set; }
    public virtual List<Cart> Carts { get; set; }
    public virtual List<Notification> Notifications { get; set; } = default!;
}