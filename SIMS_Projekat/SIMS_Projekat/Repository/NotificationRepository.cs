using SIMS_Projekat.Serialization;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class NotificationRepository
    {
        public List<Notification> Notifications { get; set; }
  

        private Serializer<Notification> notificationSerializer;

        private string notificationFile;

        private int ID;

        public NotificationRepository(string notificationFileName)
        {
            Notifications = new List<Notification>();
            notificationSerializer = new Serializer<Notification>();
            notificationFile = notificationFileName;
            ID = 100;
        }

        public void Serialize()
        {
            notificationSerializer.toCSV(notificationFile, Notifications);
        }

        public void Deserialize()
        {
            Notifications = notificationSerializer.fromCSV(notificationFile);

            int maxID = 100;
            foreach(Notification notification in Notifications)
            {
                if (int.Parse(notification.ID) > maxID)
                    maxID = int.Parse(notification.ID);
            }
            ID = ++maxID;
        }
        public Notification AddNotification(Notification notification)
        {
            notification.ID = ID++.ToString();
            Notifications.Add(notification);
            return notification;
        }

        public Notification DeleteNotification(Notification notification)
        {
            return Notifications.Remove(notification) ? notification : null;
        }

        public List<Notification> GetAllNotifications()
        {
            return Notifications;
        }

        public Notification GetNotificationByID(string notificationID)
        {
            foreach (Notification notification in Notifications)
            {
                if (notification.ID.Equals(notificationID))
                    return notification;
            }
            return null;
        }
    }
}