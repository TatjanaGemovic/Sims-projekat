using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class NotificationService
    {
        public NotificationRepository NotificationRepository { get; set; }

        public Notification AddNotification(Notification notification)
        {
            return NotificationRepository.AddNotification(notification);
        }

        public Notification DeleteNotification(Notification notification)
        {
            return NotificationRepository.DeleteNotification(notification);
        }

        public List<Notification> GetAllNotifications()
        {
            return NotificationRepository.Notifications;
        }

        public List<Notification> GetNotificationsByRecipientID(string recipientID)
        {
            List<Notification> notifications = NotificationRepository.Notifications;
            return notifications.Where(notification => notification.RecipientID == recipientID).ToList();
            
        }

        public List<Notification> GetManagerNotifications()
        {
            //Manager ID is 0
            return GetNotificationsByRecipientID("0");
        }

        public void Serialize()
        {
            NotificationRepository.Serialize();
        }

        public void Deserialize()
        {
            NotificationRepository.Deserialize();
        }

    }
}
