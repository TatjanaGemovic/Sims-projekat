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
using System.Windows.Shapes;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for EditScheduledOperation.xaml
    /// </summary>
    public partial class EditScheduledOperation : Window
    {
        public EditScheduledOperation(Appointment app)
        {
            InitializeComponent();
            Vreme_pocetka.Text = app.beginningDate.ToString();
            Vreme_zavrsetka.Text = app.endDate.ToString();
            Tip_operacije.Text = app.operation.ToString();
            ID_operacije.Text = app.appointmentID.ToString();
            
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
            if (Tip_operacije.Text.Equals("False"))
            {
                op = false;
            }
            else
            {
                op = true;
            }

            Appointment appointment = new Appointment()
            {
                appointmentID = Int32.Parse(ID_operacije.Text),
                beginningDate = DateTime.Parse(Vreme_pocetka.Text),
                endDate = DateTime.Parse(Vreme_zavrsetka.Text),
                operation = op,
                room = room,
                doctor = doctor,
                patient = patient1
            };

            
            App.appointmentController.SetAppointment(appointment);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
