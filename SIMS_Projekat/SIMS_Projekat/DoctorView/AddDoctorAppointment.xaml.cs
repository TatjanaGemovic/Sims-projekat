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
    /// Interaction logic for AddDoctorAppointment.xaml
    /// </summary>
    public partial class AddDoctorAppointment : Page
    {
        Frame Frame;
        private String selectedDate1;
        BindingList<String> appointmentType;
        Doctor doctor;

        public AddDoctorAppointment(Frame frame, String selectedDate, Doctor d)
        {
            InitializeComponent();
            Frame = frame;
            doctor = d;
            selectedDate1 = selectedDate;
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            appointmentType = new BindingList<String>();
            appointmentType.Add("Pregled");
            appointmentType.Add("Operacija");
            Tip_operacije.ItemsSource = appointmentType;
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }

        private void Dodaj_operaciju_Click(object sender, RoutedEventArgs e)
        {
           
            Patient patient1 = new Patient()
            {
                ID = "210",
                FirstName = "Ana",
                LastName = "Anic",
                Email = "ana@gmail.com",
                Jmbg = "515120",
                Username = "ana",
                Password = "ana123",
                PhoneNumber = "0645554442",
                DateOfBirth = new DateTime(2000, 10, 15),
                BloodType = BloodType.A_Positive,
                Height = 178.0,
                Weight = 80.0,
                HealthInsuranceID = "0426"
            };

            Room room = new Room()
            {
                RoomID = "13",
                Floor = 4,
                Type = RoomType.examRoom,
                RoomNumber = 13,
                Available = false,
            };
            bool op;
            String tip = Tip_operacije.SelectionBoxItem.ToString();
            if (tip.Equals("Pregled"))
            {
                op = false;
            }
            else
            {
                op = true;
            }

            Appointment appointment = new Appointment()
            {
                //appointmentID = Int32.Parse(ID_operacije.Text),
                beginningDate = DateTime.Parse(Vreme_pocetka.Text),
                endDate = DateTime.Parse(Vreme_zavrsetka.Text),
                operation = op,
                room = room,
                doctor = doctor,
                patient = patient1
            };


            /*ScheduledOperation s = new ScheduledOperation();
            s.Start = DateTime.Parse(Vreme_pocetka.Text);
            s.End = DateTime.Parse(Vreme_zavrsetka.Text);
            s.OperationType = Tip_operacije.Text;
            s.OperationID = int.Parse(ID_operacije.Text);
            App.ScheduledOperationController.ScheduleOperation(s);*/
            App.appointmentController.AddAppointment(appointment);

            Scheduling scheduling = new Scheduling(Frame, selectedDate1, doctor);
            Frame.Content = scheduling;
        }
    }
}
