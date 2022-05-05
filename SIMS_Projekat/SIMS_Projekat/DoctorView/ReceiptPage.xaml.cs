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
    /// Interaction logic for ReceiptPage.xaml
    /// </summary>
    public partial class ReceiptPage : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private Patient patient;
        private Appointment appointment;
        BindingList<String> times;

        public ReceiptPage(Frame frame, Doctor doctor1, Patient patient1, Appointment app)
        {
            InitializeComponent();
            Frame = frame;
            doctor = doctor1;
            patient = patient1;
            appointment = app; 
            Ime_pacijenta.Text = patient.FirstName + " " + patient.LastName;
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            times = new BindingList<String>();
            times.Add("1");
            times.Add("2");
            times.Add("3");
            times.Add("4");
            Dnevno.ItemsSource = times;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ExaminationInfo(Frame, appointment, doctor);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string treatment = Tretman.Text;
            DateTime from = DateTime.Parse(Od.SelectedDate.ToString());
            DateTime to = DateTime.Parse(Do.SelectedDate.ToString());

            int broj = int.Parse(Dnevno.SelectedItem.ToString());

            Receipt receipt = new Receipt()
            {
                beginningDate = from,
                endDate = to,
                Record = treatment,
                patient = patient,
                DailyMed = broj,
                appointmentID = appointment.appointmentID
            };

            Receipt receiptWithID = App.receiptRepository.AddReceipt(receipt);
            App.receiptRepository.Serialize();
            App.therapyNotificationController.CreateNotification(receiptWithID);

            Frame.Content = new ExaminationInfo(Frame, appointment, doctor);
        }
    }
}
