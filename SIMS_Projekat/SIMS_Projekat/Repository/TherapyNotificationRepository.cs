using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class TherapyNotificationRepository
    {
        private string path { get; set; }
        private Serializer<TherapyNotification> serializer;
        private static List<TherapyNotification> notificationList;
        private int id;

        public List<TherapyNotification> NotificationList
        {
            get
            {
                if (notificationList == null)
                    notificationList = new List<TherapyNotification>();
                return notificationList;
            }
            set
            {
                RemoveAllNotifications();
                if (value != null)
                {
                    foreach (TherapyNotification notification in value)
                        AddNotification(notification);
                }
            }
        }

        public TherapyNotificationRepository(string path)
        {
            this.path = path;
            serializer = new Serializer<TherapyNotification>();
            id = 0;
        }


        public TherapyNotification SetNotification(TherapyNotification notification)
        {
            int index;
            foreach (TherapyNotification not in notificationList)
            {
                TherapyNotification notification1 = notificationList.Find(not => not.ID == notification.ID);
                index = notificationList.IndexOf(notification1);

                if (notification1 != null)
                {
                    notification1.patient = notification.patient;
                    notification1.medicineName = notification.medicineName;
                    notification1.date = notification.date;
                    notification1.receipt = notification.receipt;
                    return notification1;
                }

            }
            return null;
        }

        public TherapyNotification GetNotificationByID(int notificationID)
        {
            foreach (TherapyNotification notification in notificationList)
            {
                TherapyNotification notification1 = notificationList.Find(notification => notification.ID == notificationID);

                if (notification1 != null)
                {
                    return notification1;
                }
            }
            return null;
        }

        public static List<TherapyNotification> GetNotificationByPatientID(string patientID)
        {
            List<TherapyNotification> notificationListForPatient = new List<TherapyNotification>();
            foreach (TherapyNotification notification in notificationList)
            {
                if (notification.patient.ID.Equals(patientID))
                {
                    notificationListForPatient.Add(notification);
                }
            }
            return notificationListForPatient;
        }

        public List<TherapyNotification> GetAllNotifications()
        {
            return notificationList;
        }

        public TherapyNotification AddNotification(TherapyNotification newNotification)
        {
            id++;
            if (newNotification == null)
                return null;
            if (notificationList == null)
                notificationList = new List<TherapyNotification>();
            if (!notificationList.Contains(newNotification))
            {
                newNotification.ID = id;
                notificationList.Add(newNotification);
            }

            return newNotification;
        }
        public TherapyNotification DeleteNotification(TherapyNotification oldNotification)
        {
            if (oldNotification == null)
                return null;
            if (notificationList != null)
                if (notificationList.Contains(oldNotification))
                    notificationList.Remove(oldNotification);
            return oldNotification;
        }

        public void RemoveAllNotifications()
        {
            if (notificationList != null)
                notificationList.Clear();
        }
        public void Serialize()
        {
            serializer.toCSV(path, notificationList);
        }

        public void Deserialize()
        {
            notificationList = serializer.fromCSV(path);

            foreach (TherapyNotification notification in notificationList)
            {
                id = notification.ID;
            }
        }

    }
}
