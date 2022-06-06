using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.SecretaryView.Notifications
{
    public class NotificationSender : INotificationSender
    {
        private INotificationController notificationController;
        public NotificationSender(INotificationController notificationController)
        {
            this.notificationController = notificationController;
        }
        public void SendNotification(string recipientID, string content)
        {
            notificationController.AddNotification(new Notification
            {
                RecipientID = recipientID,
                Content = content
            });
        }
    }
}
