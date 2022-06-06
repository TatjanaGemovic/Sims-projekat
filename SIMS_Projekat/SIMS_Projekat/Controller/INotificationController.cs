using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public interface INotificationController
    {
        public Notification AddNotification(Notification notification);
        public Notification DeleteNotification(Notification notification);
        public List<Notification> GetAllNotifications();
        public List<Notification> GetNotificationsByRecipientID(string recipientID);
        public List<Notification> GetManagerNotifications();

        public void Serialize();
        public void Deserialize();
    }
}
