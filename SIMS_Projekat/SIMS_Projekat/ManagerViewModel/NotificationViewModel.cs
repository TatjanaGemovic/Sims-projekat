using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.ManagerViewModel
{
    public class NotificationViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Notification> notifications;


        public ObservableCollection<Notification> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }

        public NotificationViewModel()
        {

            Notifications = new ObservableCollection<Notification>(App.NotificationController.GetManagerNotifications());

        }


        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
