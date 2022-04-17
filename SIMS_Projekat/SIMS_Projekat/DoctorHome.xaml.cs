using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for DoctorHome.xaml
    /// </summary>
    public partial class DoctorHome : Window
    {
        public List<ScheduledOperation> operations;
        //public static ObservableCollection<ScheduledOperation> Operations { get; set; }
        public BindingList<AppointmentInformation> appointmentInformations { get; set; }
        public DoctorHome()
        {
            InitializeComponent();
            appointmentInformations = new BindingList<AppointmentInformation>();
            createList();

            OperationsList.ItemsSource = appointmentInformations;
            this.DataContext = this;
            //Operations = App.scheduledOperationRepository.GetScheduledOperations();
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }

        private void Otkazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            if (OperationsList.SelectedItem != null)
            {
                if (MessageBox.Show("Jeste li sigurni da zelite da otkazete odabrani termin?",
                "Otkazivanje termina", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;

                    int appointmentID = appointmentInformation.appointmentId;

                    Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);

                    if (appointment != null)
                    {
                        App.appointmentController.DeleteAppointment(appointment);

                        appointmentInformations.Clear();
                        createList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Niste izabrali termin za otkazivanje!", "Greska");
            }
        }

        private void Izmenite_Termin_Click(object sender, RoutedEventArgs e)
        {
            AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;
            int appointmentID = appointmentInformation.appointmentId;
            Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);
            EditScheduledOperation editScheduledOperation = new EditScheduledOperation(appointment);
           // this.Close();
            editScheduledOperation.Show();
        }

        private void Zakazite_Termin_Click(object sender, RoutedEventArgs e)
        {
            AddScheduledOperation addScheduledOperation = new AddScheduledOperation();
            //this.Close();
            addScheduledOperation.Show();
        }

        public class AppointmentInformation
        {
            public int appointmentId { get; set; }
            public string patientName { get; set; }
            public string beginningTime { get; set; }
            public string endTime { get; set; }
            public string appointmentType { get; set;}

            public AppointmentInformation(int apid, string patient, string d, string d2, string type)
            {
                appointmentId = apid;
                patientName = patient;
                beginningTime = d;
                endTime = d2;
                appointmentType = type;

            }
        }
        public void createList()
        {
            foreach (Model.Appointment appointment in App.appointmentController.GetAllAppointments())
            {
                DateTime dt = appointment.beginningDate;
                string dateTime = dt.ToString("MM/dd/yyyy HH:mm");

                DateTime dt2 = appointment.endDate;
                string dateTime2 = dt2.ToString("MM/dd/yyyy HH:mm");

                appointmentInformations.Add(new AppointmentInformation(appointment.appointmentID, appointment.patient.FirstName + " " + appointment.patient.LastName,
                                              dateTime, dateTime2, appointment.operation.ToString()));
            }
        }
    }
}
