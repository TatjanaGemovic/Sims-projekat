using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView.ViewModel
{
    public class NotificationViewModel : BindableBase
    {
        private Frame frame;
        private Doctor doctor;
        public MyICommand backCommand { get; set; }
        public MyICommand readCommand { get; set; }
        public ObservableCollection<Notification2> Notifications { get; set; }
        private Notification2 selectedNotification;

        public NotificationViewModel(Frame f, Doctor d)
        {
            frame = f;
            doctor = d;
            loadRequests();
            backCommand = new MyICommand(OnBack);
            readCommand = new MyICommand(OnRead, CanRead);
        }

        private void OnBack()
        {
            frame.Content = new DoctorAppointments(frame, doctor);
        }

        private void OnRead()
        {
            foreach (Notification n in App.NotificationController.GetNotificationsByRecipientID(doctor.LicenceNumber))
            {
                if (n.Content.Equals(SelectedNotification.text))
                    n.IsRead = true;
            }
            App.NotificationRepository.Serialize();
            Notifications.Remove(SelectedNotification);
        }

        private bool CanRead()
        {
            return SelectedNotification != null;
        }

        public Notification2 SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                selectedNotification = value;
                readCommand.RaiseCanExecuteChanged(); //try to comment 
            }
        }

        public class Notification2
        {
            public string text { get; set; }

            public Notification2(string t)
            {
                text = t;
            }
        }

        public void loadRequests()
        {
            ObservableCollection<Notification2> notifications = new ObservableCollection<Notification2>();
            foreach(Notification n in App.NotificationController.GetNotificationsByRecipientID(doctor.ID))
            {
                if(n.IsRead == false)
                    notifications.Add(new Notification2(n.Content));
            }
            Notifications = notifications;
        }
    }
}
