using SIMS_Projekat.Model;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for Scheduling.xaml
    /// </summary>
    public partial class Scheduling : Page
    {
        Frame Frame;
        private Doctor doctor;
        DateTime selectedDate1;
        string selectedDate2;
        string selectedDate;
        public BindingList<AppointmentInformation> appointmentInformations { get; set; }
        public Scheduling(Frame frame, Doctor d, DateTime selectedDate)
        {
            InitializeComponent();
            Frame = frame;
            doctor = d;
            string sd = selectedDate.ToString();
            string [] dateTime = sd.Split(" ");
            selectedDate2 = dateTime[0];
            appointmentInformations = new BindingList<AppointmentInformation>();
            createList();

            OperationsList.ItemsSource = appointmentInformations;
            this.DataContext = this;
            Date_picker.Text = sd;
        }

        private void Date_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = Date_picker.SelectedDate.ToString();
            selectedDate2 = selectedDate;
            selectedDate1 = DateTime.Parse(selectedDate);
            appointmentInformations = new BindingList<AppointmentInformation>();
            createList();

            OperationsList.ItemsSource = appointmentInformations;
            this.DataContext = this;
        }

        private async void Otkazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;

            int appointmentID = appointmentInformation.appointmentId;

            Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);

            if (appointment != null)
            {
                App.appointmentController.DeleteAppointment(appointment);
                //appointmentInformations.Clear();
                appointmentInformations.Remove(appointmentInformation);
                createList();
                DeleteDialog dialog = new DeleteDialog("Uspesno izbrisan pregled");
                dialog.Show();
                await Task.Delay(TimeSpan.FromSeconds(2));
                dialog.Close();

            }
        }

        private void Izmenite_Termin_Click(object sender, RoutedEventArgs e)
        {

            AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;
            int appointmentID = appointmentInformation.appointmentId;
            Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);
            Frame.Content = new EditDoctorAppointment(Frame, appointment, selectedDate1, doctor);
        }

        private void Zakazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new AddDoctorAppointment(Frame, selectedDate1, doctor);
        }

        public class AppointmentInformation
        {
            public int appointmentId { get; set; }
            public string patientName { get; set; }
            public string beginningTime { get; set; }
            public string endTime { get; set; }
            public string appointmentType { get; set; }

            public AppointmentInformation(int apid, string patient, string d, string d2, string type)
            {
                appointmentId = apid;
                patientName = patient;
                beginningTime = d;
                endTime = d2;
                appointmentType = type;

            }
        }
        public void createList()
        {
            appointmentInformations = App.listsForBinding.CreatePatientList(doctor, selectedDate2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        private void OperationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OperationsList.SelectedItem != null)
            {
                Izmenite_Termin.IsEnabled = true;
                Otkazite_Termin.IsEnabled = true;
            }
        }

    }
}
