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
            createList();

            AppointmentsTable.ItemsSource = appointmentInformations;
            this.DataContext = this;
        }

        private void make_appointment_Click(object sender, RoutedEventArgs e)
        {
            ScheduleAppointmentPage ScheduleAppointmentPage = new ScheduleAppointmentPage(frame, patient);
            frame.Content = ScheduleAppointmentPage;
        }
        private void show_appointment_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsTable.SelectedItem != null)
            {
                AppointmentInformation appointmentInformation = (AppointmentInformation)AppointmentsTable.SelectedItem;

                int appointmentID = appointmentInformation.appointmentId;

                ViewAppointmentPage viewAppointmentPage = new ViewAppointmentPage(frame, appointmentID, patient);
                frame.Content = viewAppointmentPage;
            }
            else
            {
                MessageBox.Show("Niste izabrali termin za prikaz!", "Greska");
            }

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
        public void createList()
        {
            foreach (Model.Appointment appointment in App.appointmentController.GetAppointmentByPatientID(patient.ID))
            {
                    DateTime dt = appointment.beginningDate;
                    string dateTime = dt.ToString("MM/dd/yyyy HH:mm");

                    String[] deloviDatuma = dateTime.Split(" ");
                    string datum = deloviDatuma[0];

                    string vreme = deloviDatuma[1];

                    appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                                  appointment.doctor.FirstName + " " + appointment.doctor.LastName, datum, vreme, appointment.room.RoomNumber, appointment.operation));
                
            }
        }
    }
}
