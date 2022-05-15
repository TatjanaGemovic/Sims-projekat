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

        private void showAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            appointment = App.appointmentController.CreateRandomAppointment(patient);

            doctorContentLabel.Content = appointment.doctor.FirstName + " " + appointment.doctor.LastName;
            dateContentLabel.Content = appointment.beginningDate.Date.ToString("dd.MM.yyyy.");
            timeContentLabel.Content = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");
        }

        private void scheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            App.appointmentController.AddAppointment(appointment);

            Appointments Appointments = new Appointments(mainFrame, patient);
            mainFrame.Content = Appointments;
        }
    }
}
