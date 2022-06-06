using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class NotificationController : INotificationController
    {
        public NotificationService NotificationService { get; set; }

        public Notification AddNotification(Notification notification)
        {
            return NotificationService.AddNotification(notification);
        }

        public Notification DeleteNotification(Notification notification)
        {
            return NotificationService.DeleteNotification(notification);
        }

        public List<Notification> GetAllNotifications()
        {
            return NotificationService.GetAllNotifications();
        }

        public List<Notification> GetNotificationsByRecipientID(string recipientID)
        {
            return NotificationService.GetNotificationsByRecipientID(recipientID);
        }

        public List<Notification> GetManagerNotifications()
        {
            return NotificationService.GetManagerNotifications();
        }


        public void Serialize()
        {
            NotificationService.Serialize();
        }

        public void Deserialize()
        {
            NotificationService.Deserialize();
        }
    }
}
