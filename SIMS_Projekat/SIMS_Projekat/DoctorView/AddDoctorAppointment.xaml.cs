using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
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
        BindingList<String> patients;
        BindingList<String> rooms;
        private List<Patient> patients2;
        Doctor doctor;

        public AddDoctorAppointment(Frame frame, String selectedDate, Doctor d)
        {
            InitializeComponent();
            Frame = frame;
            doctor = d;
            selectedDate1 = selectedDate;
            InitializeComboBox1();
            InitializeComboBox2();
            InitializeComboBox3();
        }

        private void InitializeComboBox1()
        {
            appointmentType = new BindingList<String>();
            appointmentType.Add("Pregled");
            appointmentType.Add("Operacija");
            Tip_operacije.ItemsSource = appointmentType;
        }

        private void InitializeComboBox2()
        {
            patients = new BindingList<String>();
            patients2 = App.accountController.GetAllPatientAccounts();

            foreach(Patient p in patients2)
            {
                patients.Add(p.FirstName + " " + p.Username);
            }
            Ime_pacijenta.ItemsSource = patients;
        }

        private void InitializeComboBox3()
        {
            rooms = new BindingList<String>();
            foreach(Room r in App.roomController.GetRooms())
            {
                rooms.Add(r.RoomNumber.ToString());
            }
            Ime_sobe.ItemsSource = rooms;
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }

        private void Dodaj_operaciju_Click(object sender, RoutedEventArgs e)
        {
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
            Patient patient1 = new Patient();
            String patient = Ime_pacijenta.SelectionBoxItem.ToString();
            foreach(Patient p in App.accountController.GetAllPatientAccounts())
            {
                string str = p.FirstName + " " + p.Username;
                if (str.Equals(patient)){
                    patient1 = p;
                }
            }
            Room room1 = new Room();
            String id = Ime_sobe.SelectionBoxItem.ToString();
            foreach(Room r in App.roomController.GetRooms())
            {
                if(r.RoomNumber == int.Parse(id))
                {
                    room1 = r;
                }
            }

            Appointment appointment = new Appointment()
            {
                //appointmentID = Int32.Parse(ID_operacije.Text),
                beginningDate = DateTime.Parse(Vreme_pocetka.Text),
                endDate = DateTime.Parse(Vreme_zavrsetka.Text),
                operation = op,
                room = room1,
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

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Scheduling scheduling = new Scheduling(Frame, selectedDate1, doctor);
            Frame.Content = scheduling;
        }
    }
}
