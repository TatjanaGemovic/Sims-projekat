using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PatientInfo.xaml
    /// </summary>
    public partial class PatientInfo : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private Patient patient;

        public PatientInfo(Frame frame, Doctor doctor1, Patient patient1)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            patient = patient1;
            Ime_pacijenta.Text = patient1.FirstName + " " + patient1.LastName;
            Ime.Text = patient.FirstName;
            Prezime.Text = patient.LastName;
            JMBG.Text = patient.Jmbg;

            string time = patient.DateOfBirth.ToString();
            String[] datePart = time.Split(" ");
            Datum_rodjenja.Text = datePart[0];
            Email.Text = patient.Email;
            Telefon.Text = patient.PhoneNumber;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new PatientList(Frame, doctor);
        }

        private void MedicalCarton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new PatientCard(Frame, doctor, patient);
        }

        private void Examination_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new PatientExaminationList(Frame, doctor, patient);
        }
    }
}
