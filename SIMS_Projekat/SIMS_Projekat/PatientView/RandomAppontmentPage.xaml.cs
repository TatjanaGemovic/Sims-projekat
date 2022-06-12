using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RandomAppontmentFirstPage.xaml
    /// </summary>
    public partial class RandomAppontmentFirstPage : Page
    {
        Patient patient;
        Frame mainFrame;
        Appointment appointment;
        public RandomAppontmentFirstPage(Patient p, Frame frame)
        {
            patient = p;
            mainFrame = frame;
            InitializeComponent();
            appointment = App.appointmentController.CreateRandomAppointment(patient);
            doctorContentLabel.Content = appointment.doctor.FirstName + " " + appointment.doctor.LastName;
            dateContentLabel.Content = appointment.beginningDate.Date.ToString("dd.MM.yyyy.");
            timeContentLabel.Content = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
        }

        private void ShowAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            appointment = App.appointmentController.CreateRandomAppointment(patient);

            doctorContentLabel.Content = appointment.doctor.FirstName + " " + appointment.doctor.LastName;
            dateContentLabel.Content = appointment.beginningDate.Date.ToString("dd.MM.yyyy.");
            timeContentLabel.Content = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            int reminderID = CreateReminderPage(appointment.beginningDate);
            appointment.reminderForPatientID = reminderID;
            App.appointmentController.AddAppointment(appointment);

            Appointments Appointments = new Appointments(mainFrame, patient);
            mainFrame.Content = Appointments;
        }

        private int CreateReminderPage(DateTime date)
        {
            Reminder newReminder = new Reminder()
            {
                isRepeatable = "Nikada",
                patient = patient,
                startTime = date.AddDays(-1),
                type = "Pregled: ",
                content = "sutra u " + date.TimeOfDay.ToString(@"hh\:mm")
            };
            newReminder = App.reminderController.AddReminder(newReminder);
            return newReminder.ID;
        }

        public void DemoExecution()
        {
            Task.Delay(1500).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     showAppointmentButton.Background = new SolidColorBrush(Color.FromRgb(173, 206, 116));
                 }
                ));
            });
            Task.Delay(3000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     showAppointmentButton.Background = new SolidColorBrush(Color.FromRgb(56, 102, 65));
                     appointment = App.appointmentController.CreateRandomAppointment(patient);
                     doctorContentLabel.Visibility = Visibility.Hidden;
                     dateContentLabel.Visibility = Visibility.Hidden;
                     timeContentLabel.Visibility = Visibility.Hidden;
                 }
                ));
            });

            Task.Delay(5000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     doctorContentLabel.Visibility = Visibility.Visible;
                     doctorContentLabel.Content = appointment.doctor.FirstName + " " + appointment.doctor.LastName;
                 }
                ));
            });
            Task.Delay(7000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     dateContentLabel.Visibility = Visibility.Visible;
                     dateContentLabel.Content = appointment.beginningDate.Date.ToString("dd.MM.yyyy.");
                 }
                ));
            });
            Task.Delay(9000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     timeContentLabel.Visibility = Visibility.Visible;
                     timeContentLabel.Content = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
                 }
                ));
            });

        }
    }
}
