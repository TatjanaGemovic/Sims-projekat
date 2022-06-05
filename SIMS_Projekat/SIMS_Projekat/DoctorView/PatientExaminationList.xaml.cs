 using SIMS_Projekat.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for PatientExaminationList.xaml
    /// </summary>
    public partial class PatientExaminationList : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private Patient patient;
        public BindingList<FinishedAppointment2> patientFinishedAppointmentList { get; set; }
        public PatientExaminationList(Frame frame, Doctor doctor1, Patient patient1)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            patient = patient1;
            patientFinishedAppointmentList = new BindingList<FinishedAppointment2>();
            createList();
            PatientExaminatinLists.ItemsSource = patientFinishedAppointmentList;
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new PatientInfo(Frame, doctor, patient);
        }

        public class FinishedAppointment2
        {
            public int id { get; set; }
            public string beginningDate { get; set; }

            public string time { get; set; }
            public string doctorName { get; set; }

            public FinishedAppointment2(int id, string beginningDate, string doctorName, string time)
            {
                this.id = id;
                this.beginningDate = beginningDate;
                this.doctorName = doctorName;
                this.time = time;
            }

        }

        public void createList()
        {
            patientFinishedAppointmentList = App.listsForBinding.CreateExaminationListForPatient(patient);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                FinishedAppointment2 appointmentInformation = (FinishedAppointment2)PatientExaminatinLists.SelectedItem;
                int appointmentID = appointmentInformation.id;
                FinishedAppointment appointment = App.finishedAppointmentRepo.GetAppointmentByID(appointmentID);
                Frame.Content = new PatientExaminationInfo(Frame, doctor, appointment);
        }

        private void PatientExaminatinLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientExaminatinLists.SelectedItem != null)
            {
                Details.IsEnabled = true;
            }
        }
    }
}
