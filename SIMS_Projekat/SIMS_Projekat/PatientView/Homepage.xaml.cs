using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Page, INotifyPropertyChanged
    {
        private Patient patient;

        private ObservableCollection<TherapyNotification> notificationCollection;

        public ObservableCollection<TherapyNotification> NotificationCollection
        {
            get { return notificationCollection; }
            set
            {
                if (value != notificationCollection)
                {
                    notificationCollection = value;
                    OnPropertyChanged("NotificationCollection");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Homepage(Patient p)
        {
            InitializeComponent();
            patient = p;
            this.DataContext = this;
            NotificationCollection = App.therapyNotificationService.GetActiveNotifications();
        }

       
    }
}
 