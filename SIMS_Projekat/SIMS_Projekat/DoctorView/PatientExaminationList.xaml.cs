using SIMS_Projekat.Model;
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
            public string doctorName { get; set; }

            public FinishedAppointment2(int id, string beginningDate, string doctorName)
            {
                this.id = id;
                this.beginningDate = beginningDate;
                this.doctorName = doctorName;
            }

        }

        public void createList()
        {
            if (App.finishedappointmentRepo.GetAllAppointments() != null)
            {
                foreach (FinishedAppointment app in App.finishedappointmentRepo.GetAllAppointments())
                {
                    if (app.patient.ID.Equals(patient.ID))
                    {
                        int id = app.finishedAppointmentID;
                        string time = app.beginningDate.Date.ToString();
                        string name = app.doctor.FirstName + " " + app.doctor.LastName;

                        patientFinishedAppointmentList.Add(new FinishedAppointment2(id, time, name));
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (PatientExaminatinLists.SelectedItem != null)
            {
                FinishedAppointment2 appointmentInformation = (FinishedAppointment2)PatientExaminatinLists.SelectedItem;
                int appointmentID = appointmentInformation.id;
                FinishedAppointment appointment = App.finishedappointmentRepo.GetAppointmentByID(appointmentID);
                Frame.Content = new PatientExaminationInfo(Frame, doctor, appointment);
            }
            else
            {
                MessageBox.Show("Niste izabrali pregled za prikaz!", "Greska");
            }
        }
    }
}
