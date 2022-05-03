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
    /// Interaction logic for ExaminationInfo.xaml
    /// </summary>
    public partial class ExaminationInfo : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private Appointment appointment; 
        public ExaminationInfo(Frame frame, Appointment app, Doctor doctor1)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            appointment = app;
            Ime_pacijenta.Text = app.patient.FirstName + " " + app.patient.LastName;

            string time = app.beginningDate.ToString();
            String[] datePart = time.Split(" ");
            Datum.Text =  datePart[0];
            Vreme.Text = app.beginningDate.TimeOfDay.ToString();
            Soba.Text = app.room.RoomNumber.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Examinations(Frame, doctor);
        }

        private void Anamnesis_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Anamnesis(Frame, appointment, doctor);
        }

        private void Zavrsi_Pregled_Click(object sender, RoutedEventArgs e)
        {
            App.appointmentController.DeleteAppointment(appointment);
            Frame.Content = new Examinations(Frame, doctor);
        }

        private void Receipt_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ReceiptPage(Frame, doctor, appointment.patient, appointment);
        }
    }
}
