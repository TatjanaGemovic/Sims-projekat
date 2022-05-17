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
    /// Interaction logic for PatientCard.xaml
    /// </summary>
    public partial class PatientCard : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private Patient patient;

        public PatientCard(Frame frame, Doctor doctor1, Patient patient1)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            patient = patient1;
            Ime_pacijenta.Text = patient1.FirstName + " " + patient1.LastName;
            TrenTerapija.Text = patient1.MedicalRecord.CurrentTherapy;
            Dodatno.Text = patient1.MedicalRecord.Note;
            Hospitalizacija.Text = patient1.MedicalRecord.BeenHospitalized.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new PatientInfo(Frame, doctor, patient);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string ter = TrenTerapija.Text;
            string note = Dodatno.Text;
            string hosp = Hospitalizacija.Text;
            bool hosp2;
            if (hosp.Equals("False"))
            {
                hosp2 = false;
            }
            else
            {
                hosp2 = true;
            }

            string id = patient.MedicalRecord.ID;

            MedicalRecord record = new MedicalRecord()
            {
                ID = id,
                CurrentTherapy = ter,
                Note = note,
                BeenHospitalized = hosp2,
            };

            App.medRecordRepository.SetMedicalRecord(record);
            App.medRecordRepository.Serialize();
        }
    }
}
