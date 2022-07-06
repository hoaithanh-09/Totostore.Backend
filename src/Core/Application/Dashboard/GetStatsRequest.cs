using Totostore.Backend.Application.Identity.Roles;
using Totostore.Backend.Application.Identity.Users;

namespace Totostore.Backend.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Supplier> _supplierRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer<GetStatsRequestHandler> _localizer;

    public GetStatsRequestHandler(IUserService userService, IRoleService roleService, IReadRepository<Supplier> supplierRepo, IReadRepository<Product> productRepo, IStringLocalizer<GetStatsRequestHandler> localizer)
    {
        _userService = userService;
        _roleService = roleService;
        _supplierRepo = supplierRepo;
        _productRepo = productRepo;
        _localizer = localizer;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            ProductCount = await _productRepo.CountAsync(cancellationToken),
            SupplierCount = await _supplierRepo.CountAsync(cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken)
        };

        int selectedYear = DateTime.Now.Year;
        double[] productsFigure = new double[13];
        double[] suppliersFigure = new double[13];
        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01);
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

            var supplierSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Supplier>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);

            suppliersFigure[i - 1] = await _supplierRepo.CountAsync(supplierSpec, cancellationToken);
            productsFigure[i - 1] = await _productRepo.CountAsync(productSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Products"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Suppliers"], Data = suppliersFigure });

        return stats;
    }
}