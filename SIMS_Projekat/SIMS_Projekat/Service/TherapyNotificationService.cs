using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class TherapyNotificationService
    {
        public TherapyNotificationRepository therapyNotificationRepository;

        public TherapyNotification SetNotification(TherapyNotification notification)
        {
            return therapyNotificationRepository.SetNotification(notification);
        }

        public TherapyNotification GetNotificationByID(int notificationtID)
        {
            return therapyNotificationRepository.GetNotificationByID(notificationtID);
        }

        public List<TherapyNotification> GetNotificationByPatientID(string patientID)
        {
            return therapyNotificationRepository.GetNotificationByPatientID(patientID);
        }

        public List<TherapyNotification> GetAllNotifications()
        {
            return therapyNotificationRepository.GetAllNotifications();
        }

        public TherapyNotification AddNotification(TherapyNotification newNotification)
        {
            return therapyNotificationRepository.AddNotification(newNotification);
        }
        public TherapyNotification DeleteNotification(TherapyNotification oldNotification)
        {
            return therapyNotificationRepository.DeleteNotification(oldNotification);
        }
    }
}
