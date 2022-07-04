using System.Collections.ObjectModel;

namespace Totostore.Backend.Shared.Authorization;

public static class FSHAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class FSHResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
    public const string Addresses = nameof(Addresses);
    public const string Carts = nameof(Carts);
    public const string Categories = nameof(Categories);
    public const string CategoryProducts = nameof(CategoryProducts);
    public const string Coupons = nameof(Coupons);
    public const string Customers = nameof(Customers);
    public const string Details = nameof(Details);
    public const string Notifications = nameof(Notifications);
    public const string Orders = nameof(Orders);
    public const string OrderCoupons = nameof(OrderCoupons);
    public const string OrderPayments = nameof(OrderPayments);
    public const string OrderStatus = nameof(OrderStatus);
    public const string OrderProducts = nameof(OrderProducts);
    public const string Payments = nameof(Payments);
    public const string ProductDetails = nameof(ProductDetails);
    public const string ProductImages = nameof(ProductImages);
    public const string ProductPrices = nameof(ProductPrices);
    public const string Shippers = nameof(Shippers);
    public const string Suppliers = nameof(Suppliers);
}

public static class FSHPermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", FSHAction.View, FSHResource.Dashboard),
        new("View Hangfire", FSHAction.View, FSHResource.Hangfire),
        new("View Users", FSHAction.View, FSHResource.Users),
        new("Search Users", FSHAction.Search, FSHResource.Users),
        new("Create Users", FSHAction.Create, FSHResource.Users),
        new("Update Users", FSHAction.Update, FSHResource.Users),
        new("Delete Users", FSHAction.Delete, FSHResource.Users),
        new("Export Users", FSHAction.Export, FSHResource.Users),

        new("View UserRoles", FSHAction.View, FSHResource.UserRoles),
        new("Update UserRoles", FSHAction.Update, FSHResource.UserRoles),

        new("View Roles", FSHAction.View, FSHResource.Roles),
        new("Create Roles", FSHAction.Create, FSHResource.Roles),
        new("Update Roles", FSHAction.Update, FSHResource.Roles),
        new("Delete Roles", FSHAction.Delete, FSHResource.Roles),

        new("View RoleClaims", FSHAction.View, FSHResource.RoleClaims),
        new("Update RoleClaims", FSHAction.Update, FSHResource.RoleClaims),

        new("View Products", FSHAction.View, FSHResource.Products, IsBasic: true),
        new("Search Products", FSHAction.Search, FSHResource.Products, IsBasic: true),
        new("Create Products", FSHAction.Create, FSHResource.Products),
        new("Update Products", FSHAction.Update, FSHResource.Products),
        new("Delete Products", FSHAction.Delete, FSHResource.Products),
        new("Export Products", FSHAction.Export, FSHResource.Products),

        new("View Brands", FSHAction.View, FSHResource.Brands, IsBasic: true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands, IsBasic: true),
        new("Create Brands", FSHAction.Create, FSHResource.Brands),
        new("Update Brands", FSHAction.Update, FSHResource.Brands),
        new("Delete Brands", FSHAction.Delete, FSHResource.Brands),
        new("Generate Brands", FSHAction.Generate, FSHResource.Brands),
        new("Clean Brands", FSHAction.Clean, FSHResource.Brands),

        new("View Addresses", FSHAction.View, FSHResource.Addresses, IsBasic: true),
        new("Search Addresses", FSHAction.Search, FSHResource.Addresses, IsBasic: true),
        new("Create Addresses", FSHAction.Create, FSHResource.Addresses),
        new("Update Addresses", FSHAction.Update, FSHResource.Addresses),
        new("Delete Addresses", FSHAction.Delete, FSHResource.Addresses),

        new("View Carts", FSHAction.View, FSHResource.Carts, IsBasic: true),
        new("Search Carts", FSHAction.Search, FSHResource.Carts, IsBasic: true),
        new("Create Carts", FSHAction.Create, FSHResource.Carts),
        new("Update Carts", FSHAction.Update, FSHResource.Carts),
        new("Delete Carts", FSHAction.Delete, FSHResource.Carts),

        new("View Categories", FSHAction.View, FSHResource.Categories, IsBasic: true),
        new("Search Categories", FSHAction.Search, FSHResource.Categories, IsBasic: true),
        new("Create Categories", FSHAction.Create, FSHResource.Categories),
        new("Update Categories", FSHAction.Update, FSHResource.Categories),
        new("Delete Categories", FSHAction.Delete, FSHResource.Categories),

        new("View CategoryProducts", FSHAction.View, FSHResource.CategoryProducts, IsBasic: true),
        new("Create CategoryProducts", FSHAction.Create, FSHResource.CategoryProducts),
        new("Delete CategoryProducts", FSHAction.Delete, FSHResource.CategoryProducts),
        new("Search CategoryProducts", FSHAction.Search, FSHResource.CategoryProducts, IsBasic: true),
        new("Update CategoryProducts", FSHAction.Update, FSHResource.CategoryProducts),

        new("View Coupons", FSHAction.View, FSHResource.Coupons, IsBasic: true),
        new("Search Coupons", FSHAction.Search, FSHResource.Coupons, IsBasic: true),
        new("Create Coupons", FSHAction.Create, FSHResource.Coupons),
        new("Update Coupons", FSHAction.Update, FSHResource.Coupons),
        new("Delete Coupons", FSHAction.Delete, FSHResource.Coupons),

        new("View Customers", FSHAction.View, FSHResource.Customers, IsBasic: true),
        new("Search Customers", FSHAction.Search, FSHResource.Customers, IsBasic: true),
        new("Create Customers", FSHAction.Create, FSHResource.Customers),
        new("Update Customers", FSHAction.Update, FSHResource.Customers),
        new("Delete Customers", FSHAction.Delete, FSHResource.Customers),

        new("View Details", FSHAction.View, FSHResource.Details, IsBasic: true),
        new("Search Details", FSHAction.Search, FSHResource.Details, IsBasic: true),
        new("Create Details", FSHAction.Create, FSHResource.Details),
        new("Update Details", FSHAction.Update, FSHResource.Details),
        new("Delete Details", FSHAction.Delete, FSHResource.Details),

        new("View Notifications", FSHAction.View, FSHResource.Notifications, IsBasic: true),
        new("Search Notifications", FSHAction.Search, FSHResource.Notifications, IsBasic: true),
        new("Create Notifications", FSHAction.Create, FSHResource.Notifications),
        new("Update Notifications", FSHAction.Update, FSHResource.Notifications),
        new("Delete Notifications", FSHAction.Delete, FSHResource.Notifications),

        new("View Orders", FSHAction.View, FSHResource.Orders, IsBasic: true),
        new("Search Orders", FSHAction.Search, FSHResource.Orders, IsBasic: true),
        new("Create Orders", FSHAction.Create, FSHResource.Orders),
        new("Update Orders", FSHAction.Update, FSHResource.Orders),
        new("Delete Orders", FSHAction.Delete, FSHResource.Orders),

        new("View OrderCoupons", FSHAction.View, FSHResource.OrderCoupons, IsBasic: true),
        new("Search OrderCoupons", FSHAction.Search, FSHResource.OrderCoupons, IsBasic: true),
        new("Create OrderCoupons", FSHAction.Create, FSHResource.OrderCoupons),
        new("Delete OrderCoupons", FSHAction.Delete, FSHResource.OrderCoupons),
        new("Update OrderCoupons", FSHAction.Update, FSHResource.OrderCoupons),

        new("View OrderPayments", FSHAction.View, FSHResource.OrderPayments, IsBasic: true),
        new("Search OrderPayments", FSHAction.Search, FSHResource.OrderPayments, IsBasic: true),
        new("Create OrderPayments", FSHAction.Create, FSHResource.OrderPayments),
        new("Delete OrderPayments", FSHAction.Delete, FSHResource.OrderPayments),
        new("Update OrderPayments", FSHAction.Update, FSHResource.OrderPayments),

        new("View OrderStatus", FSHAction.View, FSHResource.OrderStatus, IsBasic: true),
        new("Search OrderStatus", FSHAction.Search, FSHResource.OrderStatus, IsBasic: true),
        new("Create OrderStatus", FSHAction.Create, FSHResource.OrderStatus),
        new("Delete OrderStatus", FSHAction.Delete, FSHResource.OrderStatus),
        new("Update OrderStatus", FSHAction.Update, FSHResource.OrderStatus),

        new("View OrderProducts", FSHAction.View, FSHResource.OrderProducts, IsBasic: true),
        new("Search OrderProducts", FSHAction.Search, FSHResource.OrderProducts, IsBasic: true),
        new("Create OrderProducts", FSHAction.Create, FSHResource.OrderProducts),
        new("Delete OrderProducts", FSHAction.Delete, FSHResource.OrderProducts),
        new("Update OrderProducts", FSHAction.Update, FSHResource.OrderProducts),

        new("View Payments", FSHAction.View, FSHResource.Payments, IsBasic: true),
        new("Search Payments", FSHAction.Search, FSHResource.Payments, IsBasic: true),
        new("Create Payments", FSHAction.Create, FSHResource.Payments),
        new("Update Payments", FSHAction.Update, FSHResource.Payments),
        new("Delete Payments", FSHAction.Delete, FSHResource.Payments),

        new("View ProductDetails", FSHAction.View, FSHResource.ProductDetails, IsBasic: true),
        new("Search ProductDetails", FSHAction.Search, FSHResource.ProductDetails, IsBasic: true),
        new("Create ProductDetails", FSHAction.Create, FSHResource.ProductDetails),
        new("Delete ProductDetails", FSHAction.Delete, FSHResource.ProductDetails),
        new("Update ProductDetails", FSHAction.Update, FSHResource.ProductDetails),

        new("View ProductImages", FSHAction.View, FSHResource.ProductImages, IsBasic: true),
        new("Search ProductImages", FSHAction.Search, FSHResource.ProductImages, IsBasic: true),
        new("Create ProductImages", FSHAction.Create, FSHResource.ProductImages),
        new("Delete ProductImages", FSHAction.Delete, FSHResource.ProductImages),
        new("Update ProductImages", FSHAction.Update, FSHResource.ProductImages),

        new("View ProductPrices", FSHAction.View, FSHResource.ProductPrices, IsBasic: true),
        new("Search ProductPrices", FSHAction.Search, FSHResource.ProductPrices, IsBasic: true),
        new("Create ProductPrices", FSHAction.Create, FSHResource.ProductPrices),
        new("Delete ProductPrices", FSHAction.Delete, FSHResource.ProductPrices),
        new("Update ProductPrices", FSHAction.Update, FSHResource.ProductPrices),

        new("View Shippers", FSHAction.View, FSHResource.Shippers, IsBasic: true),
        new("Search Shippers", FSHAction.Search, FSHResource.Shippers, IsBasic: true),
        new("Create Shippers", FSHAction.Create, FSHResource.Shippers),
        new("Update Shippers", FSHAction.Update, FSHResource.Shippers),
        new("Delete Shippers", FSHAction.Delete, FSHResource.Shippers),

        new("View Suppliers", FSHAction.View, FSHResource.Suppliers, IsBasic: true),
        new("Search Suppliers", FSHAction.Search, FSHResource.Suppliers, IsBasic: true),
        new("Create Suppliers", FSHAction.Create, FSHResource.Suppliers),
        new("Update Suppliers", FSHAction.Update, FSHResource.Suppliers),
        new("Delete Suppliers", FSHAction.Delete, FSHResource.Suppliers),

        new("View Tenants", FSHAction.View, FSHResource.Tenants, IsRoot: true),
        new("Create Tenants", FSHAction.Create, FSHResource.Tenants, IsRoot: true),
        new("Update Tenants", FSHAction.Update, FSHResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", FSHAction.UpgradeSubscription, FSHResource.Tenants, IsRoot: true)
    };

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record FSHPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}