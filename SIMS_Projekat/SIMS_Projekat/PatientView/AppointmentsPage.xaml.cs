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
        Frame Frame;
        public BindingList<AppointmentInformation> appointmentInformations { get; set; }
        public Appointments(Frame MainFrame)
        {
           
            InitializeComponent();
            Frame = MainFrame;
            
            appointmentInformations = new BindingList<AppointmentInformation>();
            createList();

            AppointmentsTable.ItemsSource = appointmentInformations;
            this.DataContext = this;
        }

        private void zakazi_termin_Click(object sender, RoutedEventArgs e)
        {
            ScheduleAppointmentPage ScheduleAppointmentPage = new ScheduleAppointmentPage(Frame);
            Frame.Content = ScheduleAppointmentPage;
        }
        private void izmeni_termin_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsTable.SelectedItem != null)
            {
                AppointmentInformation appointmentInformation = (AppointmentInformation)AppointmentsTable.SelectedItem;

                int appointmentID = appointmentInformation.appointmentId;

                ChangeAppointmentPage changeAppointmentPage = new ChangeAppointmentPage(Frame, appointmentID);
                Frame.Content = changeAppointmentPage;
            }
            else
            {
                MessageBox.Show("Niste izabrali termin za izmenu!", "Greska");
            }

        }
        private void obrisi_termin_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsTable.SelectedItem != null)
            {
                if (MessageBox.Show("Jeste li sigurni da zelite da otkazete odabrani termin?",
                "Otkazivanje termina", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    AppointmentInformation appointmentInformation = (AppointmentInformation)AppointmentsTable.SelectedItem;

                    int appointmentID = appointmentInformation.appointmentId;

                    Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);


                    if(appointment != null)
                    {
                        App.appointmentController.DeleteAppointment(appointment);

                        appointmentInformations.Clear();
                        createList();
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Niste izabrali termin za otkazivanje!","Greska");
            }
        }
        public class AppointmentInformation{
            public int appointmentId { get; set; }
            public string patientName { get; set; }
            public string doctorName { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public int roomNumber { get; set; }

            public AppointmentInformation(int apid, string patient, string doctor, string d,string t, int room) { 
                appointmentId = apid;
                patientName = patient;
                doctorName = doctor;
                date = d;
                time = t;
                roomNumber = room;
            }
        }
        public void createList()
        {
            foreach (Model.Appointment appointment in App.appointmentController.GetAllAppointments())
            {
                if (!appointment.operation)
                {
                    DateTime dt = appointment.beginningDate;
                    string dateTime = dt.ToString("MM/dd/yyyy HH:mm");

                    String[] deloviDatuma = dateTime.Split(" ");
                    string datum = deloviDatuma[0];

                    string vreme = deloviDatuma[1];

                    appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                                  appointment.doctor.FirstName + " " + appointment.doctor.LastName, datum, vreme, appointment.room.RoomNumber));
                }
            }
        }
    }
}
