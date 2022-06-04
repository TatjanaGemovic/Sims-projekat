using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for PatientList.xaml
    /// </summary>
    public partial class PatientList : Page
    {
        private Doctor doctor;
        Frame Frame;
        public List<Patients> patientList { get; set; }
        public ObservableCollection<Patients> listOfPatients = new ObservableCollection<Patients>();
        public PatientList(Frame mainFrame, Doctor doctor1)
        {
            InitializeComponent();
            Frame = mainFrame;
            doctor = doctor1;
            patientList = new List<Patients>();
            createList();
            //this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Patients patient = (Patients)PatientLists.SelectedItem;
            Patient patient1 = new Patient();
            foreach (Patient p in App.accountController.GetAllPatientAccounts())
            {
                if (p.FirstName + " " + p.LastName == patient.fullName)
                {
                    patient1 = p;
                    break;
                }
            }
            Frame.Content = new PatientInfo(Frame, doctor, patient1);
        }

        public class Patients
        {
            public string fullName { get; set; }
            public Patients(String fullName1)
            {
                fullName = fullName1;
            }
        }

        public void createList()
        {
            foreach (Model.Patient patient in App.accountRepository.GetAllPatientAccounts())
            {
                string name = patient.FirstName + " " + patient.LastName;

                patientList.Add(new Patients(name));
            }
            PatientLists.ItemsSource = patientList;
        }

        private void PatientLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientLists.SelectedItem != null)
            {
                Show.IsEnabled = true;
            }
        }
    }
}
