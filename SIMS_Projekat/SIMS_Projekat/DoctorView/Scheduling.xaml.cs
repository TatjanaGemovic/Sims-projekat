using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public String selectedDate1;
        public BindingList<AppointmentInformation> appointmentInformations { get; set; }
        public Scheduling(Frame frame, String selectedDate, Doctor d)
        {
            InitializeComponent();
            Frame = frame;
            doctor = d;
            selectedDate1 = selectedDate;
            appointmentInformations = new BindingList<AppointmentInformation>();
            createList();

            Datum.Text = selectedDate.ToString();
            OperationsList.ItemsSource = appointmentInformations;
            this.DataContext = this;
        }

        private void Otkazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            if (OperationsList.SelectedItem != null)
            {
                if (MessageBox.Show("Jeste li sigurni da zelite da otkazete odabrani termin?",
                "Otkazivanje termina", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;

                    int appointmentID = appointmentInformation.appointmentId;

                    Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);

                    if (appointment != null)
                    {
                        App.appointmentController.DeleteAppointment(appointment);

                        appointmentInformations.Clear();
                        createList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Niste izabrali termin za otkazivanje!", "Greska");
            }
        }

        private void Izmenite_Termin_Click(object sender, RoutedEventArgs e)
        {
            if (OperationsList.SelectedItem != null)
            {
                    AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;
                    int appointmentID = appointmentInformation.appointmentId;
                    Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);
                    Frame.Content = new EditDoctorAppointment(Frame, appointment, selectedDate1, doctor);
            }
            else
            {
                MessageBox.Show("Niste izabrali termin za otkazivanje!", "Greska");
            }
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
            foreach (Model.Appointment appointment in App.appointmentController.GetAppointmentByDoctorLicenceNumber(doctor.LicenceNumber))
            {
                DateTime dt = appointment.beginningDate;
                string dateTime = dt.ToString("MM/dd/yyyy HH:mm");
                String[] datePart = dateTime.Split(" ");
                string date = datePart[0]; //datum
                String[] deoDatuma = date.Split("/");
                int mesec = int.Parse(deoDatuma[0]);
                int dan = int.Parse(deoDatuma[1]);
                string time = datePart[1]; //vreme

                String[] datePart2 = selectedDate1.Split(" ");
                String date2 = datePart2[0];
                String[] deoDatuma2 = date2.Split("/");
                int mesec2 = int.Parse(deoDatuma2[0]);
                int dan2 = int.Parse(deoDatuma2[1]);

                DateTime dt2 = appointment.endDate;
                String dateTime3 = dt2.ToString("MM/dd/yyyy HH:mm");

                String type;
                if(appointment.operation == false)
                {
                    type = "Pregled";
                }
                else
                {
                    type = "Operacija";
                }
                if (dan == dan2)
                {
                    appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                              time, dateTime3, type));
                }

                     

                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppointmentCalendar appointmentCalendar = new AppointmentCalendar(Frame, doctor);
            Frame.Content = appointmentCalendar;
        }
    }
}
