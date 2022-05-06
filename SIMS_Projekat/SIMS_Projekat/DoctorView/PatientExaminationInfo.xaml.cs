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
    /// Interaction logic for PatientExaminationInfo.xaml
    /// </summary>
    public partial class PatientExaminationInfo : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private FinishedAppointment finishedAppointment;
        public PatientExaminationInfo(Frame frame, Doctor doctor1, FinishedAppointment finishedAppointment1)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            finishedAppointment = finishedAppointment1;
            Ime_pacijenta.Text = finishedAppointment.patient.FirstName + " " + finishedAppointment.patient.LastName;
            Dijagnoza.Text = finishedAppointment.Anamnesis;
            Tretman.Text = finishedAppointment.Treatment;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new PatientExaminationList(Frame, doctor, finishedAppointment.patient);
        }
    }
}
