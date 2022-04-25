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

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for PatientList.xaml
    /// </summary>
    public partial class PatientList : Page
    {
        private Doctor doctor;
        Frame Frame;
        public BindingList<Patients> patientList{ get; set; }
        public PatientList(Frame mainFrame, Doctor doctor1)
        {
            InitializeComponent();
            Frame = mainFrame;
            doctor = doctor1;
            patientList = new BindingList<Patients>();
            createList();
            PatientLists.ItemsSource = patientList;
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;
            int appointmentID = appointmentInformation.appointmentId;
            Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);*/

            Patients patient = (Patients)PatientLists.SelectedItem;
            string id = patient.ID;
            Patient patient1 = new Patient();
            foreach(Patient p in App.accountController.GetAllPatientAccounts())
            {
                if(p.ID == id)
                {
                    patient1 = p;
                    break;
                }
            }
            Frame.Content = new PatientCard(Frame, doctor, patient1);
        }

        public class Patients
        {
            public string ID { get; set; }
            public string fullName { get; set; }
            public Patients(String ID1, String fullName1)
            {
                ID = ID1;
                fullName = fullName1;
            }
        }

        public void createList()
        {
            foreach(Model.Patient patient in App.accountRepository.GetAllPatientAccounts())
            {
                string id = patient.ID;
                string name = patient.FirstName + " " + patient.LastName;

                patientList.Add(new Patients(id, name));
            }
        }
    }
}
