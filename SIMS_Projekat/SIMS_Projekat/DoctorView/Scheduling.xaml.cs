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
        public List<ScheduledOperation> operations;
        private Doctor doctor;
        String selectedDate1;
        public BindingList<AppointmentInformation> appointmentInformations { get; set; }
        public Scheduling(Frame frame, String selectedDate)
        {
            InitializeComponent();
            Frame = frame;
            appointmentInformations = new BindingList<AppointmentInformation>();
            createList();

            Datum.Text = selectedDate.ToString();
            selectedDate1 = selectedDate.ToString();
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
                    Frame.Content = new EditDoctorAppointment(Frame, appointment, selectedDate1);
            }
            else
            {
                MessageBox.Show("Niste izabrali termin za otkazivanje!", "Greska");
            }
        }

        private void Zakazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new AddDoctorAppointment(Frame, selectedDate1);
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
            foreach (Model.Appointment appointment in App.appointmentController.GetAllAppointments())
            {
                DateTime dt = appointment.beginningDate;
                string dateTime = dt.ToString("MM/dd/yyyy HH:mm");

                DateTime dt2 = appointment.endDate;
                string dateTime2 = dt2.ToString("MM/dd/yyyy HH:mm");

                String type;
                if(appointment.operation == false)
                {
                    type = "Pregled";
                }
                else
                {
                    type = "Operacija";
                }

                appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                              dateTime, dateTime2, type));
            }
        }
    }
}
