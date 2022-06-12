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
        public Patient patient;
        private string nameSurname;
        static Timer timer;
        public static Brush brush;
        Page Homepage;
        public PatientHome(Patient p)
        {
            timer = new Timer(new TimerCallback(ReminderController.TickTimer), null, 60, 30000);
            InitializeComponent();
            patient = p;

            if (App.accountController.CheckIfItsTheBeginningOfANewMonth(patient))      //ako je novi mesec, promeni podatke u pacijentu i stavi broj otkazanih termina na 0
                ResetPatient();
                
            nameSurname = p.FirstName + " " + p.LastName;
            name_surname.Content = nameSurname;
            Homepage = new Homepage(MainFrame, patient);
            MainFrame.Content = Homepage;
        }
        private void ResetPatient()
        {
            Patient p = new Patient()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Allergens = patient.Allergens,
                Appointment = patient.Appointment,
                BloodType = patient.BloodType,
                DateOfBirth = patient.DateOfBirth,
                doctorLicenceNumber = patient.doctorLicenceNumber,
                Email = patient.Email,
                HealthInsuranceID = patient.HealthInsuranceID,
                Height = patient.Height,
                ID = patient.ID,
                IsUrgent = patient.IsUrgent,
                Jmbg = patient.Jmbg,
                MedicalRecord = patient.MedicalRecord,
                Password = patient.Password,
                Username = patient.Username,
                PhoneNumber = patient.PhoneNumber,
                Symptoms = patient.Symptoms,
                Weight = patient.Weight,
                currentYearUsableForCancellingAppointmentsByPatient = DateTime.Now.Year,
                currentMonthUsableForCancellingAppointmentsByPatient = DateTime.Now.Month,
                numberOfCancelledAppointmentsByPatientMonthly = 0,
                MedicalRecordID = patient.MedicalRecordID
            };
            App.accountController.EditPatientAccount(p, patient.ID);
        }
        private void Make_appointment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Appointments(MainFrame, patient);   
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
            App.accountRepository.Serialize();
            App.evaluationRepository.Serialize();
            App.noteRepository.Serialize();
            App.finishedAppointmentRepo.Serialize();
            App.reminderRepository.Serialize();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            App.reminderController.DeleteActiveNotifications();
            App.appointmentRepo.Serialize();
            App.accountRepository.Serialize();
            App.evaluationRepository.Serialize();
            App.noteRepository.Serialize();
            App.reminderRepository.Serialize();
            App.finishedAppointmentRepo.Serialize();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Homepage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Homepage(MainFrame, patient);
        }

        private void Choose_doctor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChooseDoctorPage(patient);
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ReportsPage(MainFrame, patient);
        }

        private void Notes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new NotesPage(MainFrame, patient);
        }

        private void Reminders_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new RemindersPage(MainFrame, patient);
        }
        private void Therapy_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TherapyPage(MainFrame, patient);
        }
        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(1500).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     brush = make_appointment.Background;
                     make_appointment.Background = new SolidColorBrush(Color.FromRgb(173, 206, 116));
                 }
                ));
            });

            Task.Delay(3500).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     make_appointment.Background = brush;
                     Appointments appointmentsPage = new Appointments(MainFrame, patient);
                     MainFrame.Content = appointmentsPage;
                     appointmentsPage.DemoExecution();
                     appointmentsPage.make_appointment.Background = new SolidColorBrush(Color.FromRgb(56, 102, 65));

                 }
                ));
            });
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content =  new ProfilePage(MainFrame, patient);

        }

      
        private void make_appointment_MouseEnter(object sender, MouseEventArgs e)
        {
            make_appointment.Background = new SolidColorBrush(Color.FromRgb(173, 206, 116));
        }

        private void make_appointment_MouseLeave(object sender, MouseEventArgs e)
        {
            make_appointment.Background = brush;
        }
    }
}
