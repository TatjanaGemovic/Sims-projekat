using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class TherapyNotificationController
    {
        public TherapyNotificationService therapyNotificationService;

        public TherapyNotification SetNotification(TherapyNotification notification)
        {
            return therapyNotificationService.SetNotification(notification);
        }

        public TherapyNotification GetNotificationByID(int notificationtID)
        {
            return therapyNotificationService.GetNotificationByID(notificationtID);
        }

        public List<TherapyNotification> GetNotificationByPatientID(string patientID)
        {
            return therapyNotificationService.GetNotificationByPatientID(patientID);
        }

        public List<TherapyNotification> GetAllNotifications()
        {
            return therapyNotificationService.GetAllNotifications();
        }

        public TherapyNotification AddNotification(TherapyNotification newNotification)
        {
            return therapyNotificationService.AddNotification(newNotification);
        }
        public TherapyNotification DeleteNotification(TherapyNotification oldNotification)
        {
            return therapyNotificationService.DeleteNotification(oldNotification);
        }
    }
}
