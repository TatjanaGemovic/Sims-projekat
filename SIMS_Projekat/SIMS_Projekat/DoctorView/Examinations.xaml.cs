using SIMS_Projekat.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for Examinations.xaml
    /// </summary>
    public partial class Examinations : Page
    {
        private Frame Frame;
        private Doctor doctor;
        public BindingList<Appointment2> AppointmentList { get; set; }

        public Examinations(Frame frame, Doctor doctor1)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            AppointmentList = new BindingList<Appointment2>();
            createList();
            PatientExaminatinLists.ItemsSource = AppointmentList;
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        public class Appointment2
        {
            public int id { get; set; }
            public string beginningDate { get; set; }
            public string time { get; set; }
            public string patientName { get; set; }

            public Appointment2(int id, string beginningDate, string patientName, string time)
            {
                this.id = id;
                this.beginningDate = beginningDate;
                this.patientName = patientName;
                this.time = time;
            }

        }
        public void createList()
        {
            if (App.appointmentRepo.GetAllAppointments() != null)
            {
                foreach (Appointment app in App.appointmentRepo.GetAllAppointments())
                {
                    if (app.doctor.Equals(doctor))
                    {
                        int id = app.appointmentID;
                        string date = app.beginningDate.ToString();
                        String[] datePart = date.Split(" ");
                        date = datePart[0];
                        string time = datePart[1];
                        string name = app.patient.FirstName + " " + app.patient.LastName;

                        AppointmentList.Add(new Appointment2(id, date, name, time));
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (PatientExaminatinLists.SelectedItem != null)
            {
                Appointment2 appointmentInformation = (Appointment2)PatientExaminatinLists.SelectedItem;
                int appointmentID = appointmentInformation.id;
                Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);
                Frame.Content = new ExaminationInfo(Frame, appointment, doctor);
                //Frame.Content = new EditDoctorAppointment(Frame, appointment, selectedDate1, doctor);
            }
            else
            {
                MessageBox.Show("Niste izabrali pregled za prikaz!", "Greska");
            }
        }
    }
}
