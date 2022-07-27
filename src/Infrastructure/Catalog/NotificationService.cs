using AutoMapper;
using Finbuckle.MultiTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totostore.Backend.Application.Catalog.Notifications;
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
    public async Task CreateNotificationForCustomers(CreateGeneralNotificationForIdsRequest request)
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
    }

    public async Task CreateNotificationForAllCustomer(InsertNotificationRequest request)
    {
        var customer = _db.Customers.ToList();
        foreach (var item in customer)
        {
            var notification = _mapper.Map<Notification>(request);
            _db.Add(notification);
        }
        await _db.SaveChangesAsync();
        QueryCacheManager.ExpireTag(typeof(Notification).FullName);
    }


}
