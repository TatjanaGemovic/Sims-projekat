using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for ScheduleAppointmentPage.xaml
    /// </summary>
    public partial class ScheduleAppointmentPage : Page
    {
        //private ComboBox izbor_vremena;
        Frame frame;
        Patient patient;
        public ScheduleAppointmentPage(Frame mainFrame, Patient p)
        {
            frame = mainFrame;
            patient = p;
            InitializeListOfAppointments();
            InitializeComponent();
           
            InitializeComboBox();
        }

        BindingList<String> listOfTakenAppointmentTime;
        BindingList<String> listOfAppointmentTime;
        private void InitializeListOfAppointments()
        {
            // Create the new BindingList of Part type.
            listOfAppointmentTime = new BindingList<String>();
            listOfTakenAppointmentTime = new BindingList<String>();

            // Allow new parts to be added, but not removed once committed.        
            listOfAppointmentTime.AllowNew = true;
            listOfAppointmentTime.AllowRemove = true;

            // Raise ListChanged events when new parts are added.
            listOfAppointmentTime.RaiseListChangedEvents = true;

            // Do not allow parts to be edited.
            listOfAppointmentTime.AllowEdit = false;
            listOfAppointmentTime.Add("08:00");
            listOfAppointmentTime.Add("08:15");
            listOfAppointmentTime.Add("08:30");
            listOfAppointmentTime.Add("08:45");
            listOfAppointmentTime.Add("09:00");
            listOfAppointmentTime.Add("09:15");
            listOfAppointmentTime.Add("09:30");
            listOfAppointmentTime.Add("09:45");
            listOfAppointmentTime.Add("10:00");
            listOfAppointmentTime.Add("10:15");
            listOfAppointmentTime.Add("10:30");
            listOfAppointmentTime.Add("10:45");
            listOfAppointmentTime.Add("11:00");
            listOfAppointmentTime.Add("11:15");
            listOfAppointmentTime.Add("11:30");
            listOfAppointmentTime.Add("11:45");
            listOfAppointmentTime.Add("12:00");
            listOfAppointmentTime.Add("12:15");
            listOfAppointmentTime.Add("12:30");
            listOfAppointmentTime.Add("12:45");
            listOfAppointmentTime.Add("13:00");
            listOfAppointmentTime.Add("13:15");
            listOfAppointmentTime.Add("13:30");
            listOfAppointmentTime.Add("13:45");
            listOfAppointmentTime.Add("14:00");
            listOfAppointmentTime.Add("14:15");
            listOfAppointmentTime.Add("14:30");
            listOfAppointmentTime.Add("14:45");
            listOfAppointmentTime.Add("15:00");
            listOfAppointmentTime.Add("15:15");
            listOfAppointmentTime.Add("15:30");
            listOfAppointmentTime.Add("15:45");
            listOfAppointmentTime.Add("16:00");
            listOfAppointmentTime.Add("16:15");
            listOfAppointmentTime.Add("16:30");
            listOfAppointmentTime.Add("16:45");
           


            // listOfAppointmentTime = new BindingList<Appointments>((IList<Appointments>)App.appointmentController.GetAllAppointments());
            // listOfAppointments = App.appointmentController.GetAllAppointments();

            foreach (Appointment appointment in App.appointmentController.GetAppointmentByPatientID(patient.ID))
            {
                string dateTime = appointment.beginningDate.ToString("MM/dd/yyyy HH:mm");
                String[] datePart = dateTime.Split(" ");
                string time = datePart[1];
                listOfTakenAppointmentTime.Add(time);

            }
            foreach (string time in listOfTakenAppointmentTime)
            {
                listOfAppointmentTime.Remove(time);
            }

        }
        private void InitializeComboBox()
        {

            //combovreme.DataContext = listOfAppointmentTime;
            comboTime.ItemsSource = listOfAppointmentTime;
        }

        private void scheduleClick(object sender, RoutedEventArgs e)
        {
            string datum = this.date.ToString();
            //12/07/2022 12:00:00 AM
            String[] deloviDatuma = datum.Split(" ");
            String[] deoDatuma = deloviDatuma[0].Split("/");

            string vreme = this.comboTime.SelectionBoxItem.ToString();
            String[] deloviVremena = vreme.Split(":");


            int mesec = int.Parse(deoDatuma[0]);
            int dan = int.Parse(deoDatuma[1]);
            int godina = int.Parse(deoDatuma[2]);

            int sat = int.Parse(deloviVremena[0]);
            int minut = int.Parse(deloviVremena[1]);

            Doctor doctor = new Doctor()
            {
                FirstName = "Joka",
                LastName = "Jokic",
                Email = "jok@gmail.com",
                Jmbg = "111122440",
                Username = "pera",
                Password = "pera123",
                PhoneNumber = "0641111111",
                DateOfBirth = new DateTime(1994, 5, 15),
                ID = "11",
                LicenceNumber = "1542014"
            };

            Room room = new Room()
            {
                RoomID = "13",
                Floor = 4,
                Type = RoomType.examRoom,
                RoomNumber = 13,
                Available = false,
            };

            Appointment appointment = new Appointment()
            {
                beginningDate = new DateTime(godina, mesec, dan, sat, minut, 0),
                endDate = new DateTime(godina, mesec, dan, sat, minut, 0),
                room = room,
                doctor = doctor,
                patient = patient,
                operation = false
            };

            App.appointmentController.AddAppointment(appointment);

            Appointments Appointments = new Appointments(frame, patient);
            frame.Content = Appointments;
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            Appointments Appointments = new Appointments(frame, patient);
            frame.Content = Appointments;
        }
    }
    
}
