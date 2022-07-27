using Ardalis.Specification.EntityFrameworkCore;
using Finbuckle.MultiTenant;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totostore.Backend.Application.Catalog.Products;
using Totostore.Backend.Application.Common.Models;
using Totostore.Backend.Application.Common.Specification;
using Totostore.Backend.Domain.Catalog;
using Totostore.Backend.Infrastructure.Persistence.Context;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Infrastructure.Catalog;
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _db;
    private readonly ITenantInfo _currentTenant;
  //  private readonly IMapper _mapper;
    public ProductService(ApplicationDbContext db, ITenantInfo currentTenant)
    {
        _db = db;
        _currentTenant = currentTenant;
     //   _mapper = mapper;
    }



    public async Task<PaginationResponse<ProductDetailsDto>> SearchAsync(SearchProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<Product>(request);
        var query = _db.Products.Where(x => x.Name != null);
        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        if (request.SupplierId.HasValue)
        {
            query = query.Where(x => x.SupplierId == request.SupplierId.Value);
        }

        if (request.IsSortRate.HasValue && request.IsSortRate == true)
        {
            query = query.OrderByDescending(x => x.Rate);
        }

        if (request.CategoryIds != null)
        {
            foreach (var item in request.CategoryIds)
            {
                var categoryProduct = _db.CategoryProducts.Where(x => x.CategoryId == item)
                                                         .Select(x => x.ProductId)
                                                         .ToList();

                query = query.Where(x => categoryProduct.Contains(x.Id));
            }
        }
        if (request.MinPrice.HasValue && request.MaxPrice.HasValue)
        {
            query = query.Where(x => x.ProductPrices.OrderByDescending(x => x.CreatedOn).FirstOrDefault().Amount >= request.MinPrice.Value
                                 && x.ProductPrices.OrderByDescending(x => x.CreatedOn).FirstOrDefault().Amount <= request.MaxPrice.Value);
        }
        if (request.Rate.HasValue)
        {
            query = query.Where(x => x.Rate >= request.Rate.Value && x.Rate <= 5);
        }
        var data = await query.WithSpecification(spec)
            .ProjectToType<ProductDetailsDto>()
            .ToListAsync(cancellationToken);
        int count = await _db.Products
            .CountAsync(cancellationToken);

        return new PaginationResponse<ProductDetailsDto>(data, count, request.PageNumber, request.PageSize);
    }

    public Task<ProductDetailsDto> GetById(Guid id)
    {
        throw new Exception("");
    }
}
