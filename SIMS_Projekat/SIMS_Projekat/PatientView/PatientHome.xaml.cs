using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for PatientHome.xaml
    /// </summary>
    public partial class PatientHome : Window
    {
        public static Patient patient;
        private string nameSurname;
        static Timer timer;
        Page Homepage;
        public PatientHome(Patient p)
        {
            timer = new Timer(new TimerCallback(TherapyNotificationController.TickTimer), null, 60, 30000);
            InitializeComponent();
            patient = p;
            
            nameSurname = p.FirstName + " " + p.LastName;
            name_surname.Content = nameSurname;
            Homepage = new Homepage(patient);
            MainFrame.Content = Homepage;
        }

        private void make_appointment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Appointments(MainFrame, patient);   
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
            App.accountRepository.Serialize();
            App.therapyNotificationRepository.Serialize();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            App.therapyNotificationController.DeleteActiveNotifications();
            App.appointmentRepo.Serialize();
            App.accountRepository.Serialize();
            App.therapyNotificationRepository.Serialize();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void homepage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Homepage(patient);
        }

        private void choose_doctor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChooseDoctorPage(patient);
        }


    }
}
