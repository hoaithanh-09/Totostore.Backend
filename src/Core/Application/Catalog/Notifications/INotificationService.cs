using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Notifications;
public interface INotificationService : ITransientService
{
    //Task SendNotification(Guid userId, string message, NotificationType type);
    //Task SendNotificationToAllCustomer(int departmentId, string message, NotificationType type);
    public Task CreateNotificationForCustomers(CreateGeneralNotificationForIdsRequest request);
    public Task CreateNotificationForAllCustomer(InsertNotificationRequest request);
}
