using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Finbuckle.MultiTenant;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totostore.Backend.Application.Catalog.Notifications;
using Totostore.Backend.Application.Common.Models;
using Totostore.Backend.Application.Common.Specification;
using Totostore.Backend.Domain.Catalog;
using Totostore.Backend.Infrastructure.Persistence.Context;
using Totostore.Backend.Shared.Enums;
using Z.EntityFramework.Plus;

namespace Totostore.Backend.Infrastructure.Catalog;
public class NotificationService : INotificationService
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;
    private readonly ITenantInfo _currentTenant;
    public NotificationService(ApplicationDbContext db, ITenantInfo currentTenant, IMapper mapper)
    {
        _db = db;
        _currentTenant = currentTenant;
        _mapper = mapper;
    }
    public async Task<bool> CreateNotificationForCustomers(CreateGeneralNotificationForIdsRequest request)
    {
        // var notification = _mapper.Map<Notification>(request);
        foreach (var id in request.Ids)
        {
            var notification = new Notification()
            {
                Title = request.Notification.Title,
                Content = request.Notification.Content,
                Type = request.Notification.Type,
                UserId = id,
            };
            _db.Notifications.Add(notification);
        }

        await _db.SaveChangesAsync();
        QueryCacheManager.ExpireTag(typeof(Notification).FullName);
        return true;
    }

    public async Task<bool> CreateNotificationForAllCustomer(InsertNotificationRequest request)
    {
        var customer = _db.Customers.ToList();
        foreach (var item in customer)
        {
            var notification = _mapper.Map<Notification>(request);
            _db.Add(notification);
        }
        await _db.SaveChangesAsync();
        QueryCacheManager.ExpireTag(typeof(Notification).FullName);
        return true;
    }

    public async Task<PaginationResponse<NotificationDetailsDto>> SearchAsync(SearchNotificationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<Notification>(request);
        var query = _db.Notifications.OrderBy(x => x.CreatedOn).Reverse();
        if (!string.IsNullOrEmpty(request.UserId))
        {
            query = query.Where(x => String.Equals(x.UserId, request.UserId));
        }
        if (request.Type.HasValue)
        {
            query = query.Where(x => x.Type == request.Type.Value);
        }
        if (request.FromDate.HasValue)
        {
            DateTime toDate;
            if (!request.ToDate.HasValue)
            {
                toDate = DateTime.Now;
            }
            toDate = request.ToDate.Value;
            query = query.Where(x => x.CreatedOn.CompareTo(request.FromDate.Value) >= 0 && x.CreatedOn.CompareTo(request.ToDate.Value) <= 0);
        }
        var data = await query.WithSpecification(spec)
         .ProjectToType<NotificationDetailsDto>()
         .ToListAsync(cancellationToken);
        int count = await _db.Notifications
            .CountAsync(cancellationToken);

        return new PaginationResponse<NotificationDetailsDto>(data, count, request.PageNumber, request.PageSize);
    }
}