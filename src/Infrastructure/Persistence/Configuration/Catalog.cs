using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totostore.Backend.Domain.Catalog;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Infrastructure.Persistence.Configuration;

public class SupplierConfig : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
            .HasMaxLength(256);
    }
}


public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
            .HasMaxLength(1024);
        builder
            .Property(x => x.Status)
            .HasDefaultValue(Status.Active);

        builder
            .HasOne(x => x.Supplier)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.IsMultiTenant();
        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Carts)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.Carts)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// Category
public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
            .HasMaxLength(256);
    }
}

public class CategoryProductConfig : IEntityTypeConfiguration<CategoryProduct>
{
    public void Configure(EntityTypeBuilder<CategoryProduct> builder)
    {
        builder.IsMultiTenant();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.CategoryProducts)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.ClientNoAction);

        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.CategoryProducts)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.IsMultiTenant();
        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(X => X.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Shipper)
            .WithMany(x => x.Orders)
            .HasForeignKey(X => X.ShipperId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.AddressDelivery)
            .WithMany(x => x.Orders)
            .HasForeignKey(X => X.AddressDeliveryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

// OrderStatuses
public class OrderStatusConfig : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.IsMultiTenant();
        builder
            .Property(x => x.Status)
            .HasDefaultValue(OrderStatusEnums.Pending);

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderStatuses)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// Customers
public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(c => c.Name)
            .HasMaxLength(256);
        builder
            .HasOne(x => x.Address)
            .WithMany(x => x.Customers)
            .HasForeignKey(x => x.AddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

// Notifications
public class NotificationConfig : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.IsMultiTenant();
        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Shipper)
            .WithMany(x => x.Notifications)
            .HasForeignKey(X => X.ShipperId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// Coupons
public class CouponConfig : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(c => c.Name)
            .HasMaxLength(256);
    }
}

// Details
public class ProductDetailConfig : IEntityTypeConfiguration<ProductDetail>
{
    public void Configure(EntityTypeBuilder<ProductDetail> builder)
    {
        builder.IsMultiTenant();
        builder.HasOne(x => x.Detail).WithMany(x => x.ProductDetails).HasForeignKey(x => x.DetailId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// Details
public class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.IsMultiTenant();
        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.ProductImages)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// Details
public class DetailConfig : IEntityTypeConfiguration<Detail>
{
    public void Configure(EntityTypeBuilder<Detail> builder)
    {
        builder.IsMultiTenant();
    }
}

// OrderCoupon
public class OrderCouponConfig : IEntityTypeConfiguration<OrderCoupon>
{
    public void Configure(EntityTypeBuilder<OrderCoupon> builder)
    {
        builder.IsMultiTenant();
        builder
            .HasOne(x => x.Coupon)
            .WithMany(x => x.OrderCoupons)
            .HasForeignKey(x => x.CouponId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderCoupons)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// OrderPayment
public class OrderPaymentConfig : IEntityTypeConfiguration<OrderPayment>
{
    public void Configure(EntityTypeBuilder<OrderPayment> builder)
    {
        builder.IsMultiTenant();
        builder
            .Property(x => x.Status)
            .HasDefaultValue(PaymentStatus.UnpaidInvoice);

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderPayments)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Payment)
            .WithMany(x => x.OrderPayments)
            .HasForeignKey(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

// ProductPrice
public class ProductPriceConfig : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.IsMultiTenant();
        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.ProductPrices)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
           .HasOne(x => x.Coupon)
           .WithMany(x => x.ProductPrices)
           .HasForeignKey(x => x.CouponId)
           .OnDelete(DeleteBehavior.NoAction);
    }
}

// Shipper
public class ShipperConfig : IEntityTypeConfiguration<Shipper>
{
    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(s => s.Name)
            .HasMaxLength(256);
        builder
            .HasOne(x => x.Address)
            .WithMany(x => x.Shippers)
            .HasForeignKey(x => x.AddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

// Payment
public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(p => p.Name)
            .HasMaxLength(256);
    }
}

// OrderProducts
public class OrderProductConfig : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.IsMultiTenant();
        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.OrderProducts)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderProducts)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
      
    }
}

// Addresses
public class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.IsMultiTenant();
    }
}