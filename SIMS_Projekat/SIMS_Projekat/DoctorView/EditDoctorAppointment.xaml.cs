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
    /// Interaction logic for EditDoctorAppointment.xaml
    /// </summary>
    public partial class EditDoctorAppointment : Page
    {
        Frame Frame;
        int id;
        String selectedDate1;
        BindingList<String> appointmentType;
        public EditDoctorAppointment(Frame frame, Appointment app, String selectedDate)
        {
            InitializeComponent();
            Frame = frame;
            selectedDate1 = selectedDate;
            Vreme_pocetka.Text = app.beginningDate.ToString();
            Vreme_zavrsetka.Text = app.endDate.ToString();
            //Tip_operacije.Text = app.operation.ToString();
            id = app.appointmentID;
            InitializeComboBox();

            if (app.operation.ToString().Equals("False")){
                Tip_operacije.SelectedItem = appointmentType[0];
            }
            else
            {
                Tip_operacije.SelectedItem = appointmentType[1];
            }
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
        private void Promeni_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = new Doctor()
            {
                FirstName = "Joka",
                LastName = "Jokic",
                Email = "jok@gmail.com",
                Jmbg = "111122440",
                Username = "pera",
                Password = "pera123",
                PhoneNumber = "0641111111",
                DateOfBirth = new DateTime(1994, 5, 15),
                ID = "11",
                LicenceNumber = "1542014"
            };
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
                appointmentID = id,
                beginningDate = DateTime.Parse(Vreme_pocetka.Text),
                endDate = DateTime.Parse(Vreme_zavrsetka.Text),
                operation = op,
                room = room,
                doctor = doctor,
                patient = patient1
            };

            App.appointmentController.SetAppointment(appointment);

            Scheduling scheduling = new Scheduling(Frame, selectedDate1);
            Frame.Content = scheduling;
        }
    }
}
