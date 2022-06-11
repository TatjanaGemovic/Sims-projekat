using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Appointments.xaml
    /// </summary>
    public partial class Appointments : Page
    {
        Frame frame;
        Patient patient;
        public BindingList<AppointmentInformation> appointmentInformations { get; set; }
        public Appointments(Frame mainFrame, Patient p)
        {
           
            InitializeComponent();
            frame = mainFrame;
            patient = p;
            
            appointmentInformations = new BindingList<AppointmentInformation>();
            CreateList();

            this.DataContext = this;

            CheckIfThereIsAlreadyTooManyScheduledAppointments();
        }

        private void Make_appointment_Click(object sender, RoutedEventArgs e)
        {
            ScheduleAppointmentPage ScheduleAppointmentPage = new ScheduleAppointmentPage(frame, patient);
            frame.Content = ScheduleAppointmentPage;
        }
        private void Show_appointment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentInformation appointmentInformation = (AppointmentInformation)AppointmentsTable.SelectedItem;
            ViewAppointmentPage viewAppointmentPage = new ViewAppointmentPage(frame, appointmentInformation.appointmentId, patient);
            frame.Content = viewAppointmentPage;
        }
        public class AppointmentInformation
        {
            public int appointmentId { get; set; }
            public string patientName { get; set; }
            public string doctorName { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public int roomNumber { get; set; }

            public string isOperation { get; set; }
            public AppointmentInformation(int apid, string patient, string doctor, string d,string t, int room, bool op) { 
                appointmentId = apid;
                patientName = patient;
                doctorName = doctor;
                date = d;
                time = t;
                roomNumber = room;
                if (op)
                {
                    isOperation = "Operacija";
                }
                else
                {
                    isOperation = "Pregled";
                }
            }
        }
        public void CreateList()
        {
            foreach (Appointment appointment in App.appointmentController.GetAppointmentByPatientID(patient.ID))
            {
                string dateTime = appointment.beginningDate.ToString("MM/dd/yyyy HH:mm");

                String[] deloviDatuma = dateTime.Split(" ");

                appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                                appointment.doctor.FirstName + " " + appointment.doctor.LastName, deloviDatuma[0], deloviDatuma[1], appointment.room.RoomNumber, appointment.operation));      
            }
        }
        public void CheckIfThereIsAlreadyTooManyScheduledAppointments()
        {
            DateTime now = DateTime.Now;
            if (!App.appointmentController.CheckForScheduledAppointments(patient, new DateTime(now.Year, now.Month, 1)))
            {
                make_appointment.IsEnabled = false;
            }
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
                     make_appointment.Background = new SolidColorBrush(Color.FromRgb(173, 206, 116));
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
                     ScheduleAppointmentPage ScheduleAppointmentPage = new ScheduleAppointmentPage(frame, patient);
                     frame.Content = ScheduleAppointmentPage;
                     ScheduleAppointmentPage.DemoExecution();
                 }
                ));
            });

            Task.Delay(3100).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(
               System.Windows.Threading.DispatcherPriority.Normal,
               new Action(
                 delegate ()
                 {
                     make_appointment.Background = new SolidColorBrush(Color.FromRgb(97, 177, 90));
                 }
                ));
            });

        }
    }
}
