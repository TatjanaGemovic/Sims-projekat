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
        private Frame mainFrame;
        private ObservableCollection<TherapyNotification> notificationCollection;
        private ObservableCollection<Reminder> reminderCollection;

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

        public ObservableCollection<Reminder> ReminderCollection
        {
            get { return reminderCollection; }
            set
            {
                if (value != reminderCollection)
                {
                    reminderCollection = value;
                    OnPropertyChanged("ReminderCollection");
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
        public Homepage(Frame frame,Patient p)
        {
            InitializeComponent();
            patient = p;
            mainFrame = frame;
            this.DataContext = this;
            
            NotificationCollection = App.therapyNotificationController.GetActiveNotifications();
            ReminderCollection = App.reminderController.GetActiveReminders();

            App.evaluationController.DeleteEvaluationIfMoreThanFiveDaysPassedForPatient(patient);

            if (App.evaluationController.GetEmptyEvaluationsForPatient(patient).Count == 0)
                evaluationButton.IsEnabled = false;

        }

        private void NotificationRead_Checked(object sender, RoutedEventArgs e)
        {
            if (GridView.SelectedItem != null)
            {
                TherapyNotification tn = GridView.SelectedItem as TherapyNotification;
                App.therapyNotificationController.DeleteNotification(tn);
            }
        }

        private void EvaluationButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new EvaluationPage(mainFrame, patient, App.evaluationController.GetEmptyEvaluationsForPatient(patient).Last<Evaluation>());
        }
    }
}
 