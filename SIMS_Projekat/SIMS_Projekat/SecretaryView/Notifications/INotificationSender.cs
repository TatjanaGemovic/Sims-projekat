using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.SecretaryView.Notifications
{
    public interface INotificationSender
    {
        public void SendNotification(string recipientID, string content);
    }
}
