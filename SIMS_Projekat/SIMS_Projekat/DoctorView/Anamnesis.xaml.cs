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
    /// Interaction logic for Anamnesis.xaml
    /// </summary>
    public partial class Anamnesis : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private Appointment appointment;
        public Anamnesis(Frame frame, Appointment app, Doctor doctor1)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            appointment = app;
            Ime_pacijenta.Text = app.patient.FirstName + " " + app.patient.LastName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ExaminationInfo(Frame, appointment, doctor);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string diagnosis = Dijagnoza.Text;
            string treatment = Tretman.Text;

            FinishedAppointment app = new FinishedAppointment()
            {
                beginningDate = appointment.beginningDate,
                endDate = appointment.endDate,
                operation = appointment.operation,
                doctor = doctor,
                patient = appointment.patient,
                Anamnesis = diagnosis,
                Treatment = treatment
            };

            App.finishedappointmentRepo.AddFinishedAppointment(app, appointment.appointmentID);
            App.finishedappointmentRepo.Serialize();

            Frame.Content = new ExaminationInfo(Frame, appointment, doctor);
        }
    }
}
