using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Totostore.Backend.Application.Common.Events;
using Totostore.Backend.Application.Common.Interfaces;
using Totostore.Backend.Domain.Catalog;
using Totostore.Backend.Infrastructure.Persistence.Configuration;

namespace Totostore.Backend.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser,
        ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderCoupon> OrderCoupons => Set<OrderCoupon>();
    public DbSet<CategoryProduct> CategoryProducts => Set<CategoryProduct>();
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<Detail> Details => Set<Detail>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<OrderPayment> OrderPayments => Set<OrderPayment>();
    public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
    public DbSet<OrderStatus> OrderStatus => Set<OrderStatus>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<ProductDetail> ProductDetails => Set<ProductDetail>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
    public DbSet<Shipper> Shippers => Set<Shipper>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}