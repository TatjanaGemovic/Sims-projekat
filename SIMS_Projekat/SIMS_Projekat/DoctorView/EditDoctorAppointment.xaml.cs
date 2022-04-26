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
        BindingList<String> patients;
        BindingList<String> rooms;
        BindingList<String> listofTakenAppointmentTime;
        BindingList<String> listofAppointmentTime;
        private List<Patient> patients2;
        Doctor doctor;
        public EditDoctorAppointment(Frame frame, Appointment app, String selectedDate, Doctor d)
        {
            InitializeComponent();
            Frame = frame;
            doctor = d;
            selectedDate1 = selectedDate;
            Vreme_pocetka.Text = app.beginningDate.ToString();
            Vreme_zavrsetka.Text = app.endDate.ToString();
            //Tip_operacije.Text = app.operation.ToString();
            id = app.appointmentID;
            InitializeComboBox1();
            InitializeComboBox2();
            InitializeComboBox3();
            InitializeListOfAppointments();

            if (app.operation.ToString().Equals("False")){
                Tip_operacije.SelectedItem = appointmentType[0];
            }
            else
            {
                Tip_operacije.SelectedItem = appointmentType[1];
            }
            foreach (Patient p in App.accountController.GetAllPatientAccounts())
            {
                string str = p.FirstName + " " + p.Username;
                if (str.Equals(app.patient.FirstName + " " + app.patient.LastName))
                {
                    Ime_pacijenta.Text = str;
                }
            }
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.RoomID.Equals(app.roomID))
                {
                    Ime_sobe.Text = r.RoomID;
                }
            }
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

            foreach (Patient p in patients2)
            {
                patients.Add(p.FirstName + " " + p.Username);
            }
            Ime_pacijenta.ItemsSource = patients;
        }
        private void InitializeComboBox3()
        {
            rooms = new BindingList<String>();
            foreach (Room r in App.roomController.GetRooms())
            {
                rooms.Add(r.RoomNumber.ToString());
            }
            Ime_sobe.ItemsSource = rooms;
        }

        private void InitializeListOfAppointments()
        {
            List<String> list = null;
            //ovde ide logika za slobodne termine
            listofAppointmentTime = new BindingList<String>();
            listofTakenAppointmentTime = new BindingList<String>();

            listofAppointmentTime.AllowNew = true;
            listofAppointmentTime.AllowRemove = true;

            //listofAppointmentTime.RaiseListChangedEvents = true;

            listofAppointmentTime.AllowEdit = false;
            CreateList();

            foreach (string time in listofTakenAppointmentTime)
            {
                listofAppointmentTime.Remove(time);
            }
            Vreme_pocetka.ItemsSource = listofAppointmentTime;
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
        }
        private void Promeni_Click(object sender, RoutedEventArgs e)
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
            foreach (Patient p in App.accountController.GetAllPatientAccounts())
            {
                string str = p.FirstName + " " + p.Username;
                if (str.Equals(patient))
                {
                    patient1 = p;
                }
            }
            Room room1 = new Room();
            String name = Ime_sobe.SelectionBoxItem.ToString();
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.RoomNumber == Int16.Parse(name))
                {
                    room1 = r;
                }
            }

            Appointment appointment = new Appointment()
            {
                appointmentID = id,
                beginningDate = DateTime.Parse(Vreme_pocetka.Text),
                endDate = DateTime.Parse(Vreme_zavrsetka.Text),
                operation = op,
                room = room1,
                doctor = doctor,
                patient = patient1
            };

            App.appointmentController.SetAppointment(appointment);
            App.appointmentRepo.Serialize(); 
            Scheduling scheduling = new Scheduling(Frame, selectedDate1, doctor);
            Frame.Content = scheduling;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Scheduling scheduling = new Scheduling(Frame, selectedDate1, doctor);
            Frame.Content = scheduling;
        }

        private void CreateList()
        {
            listofAppointmentTime.Add("08:00");
            listofAppointmentTime.Add("08:15");
            listofAppointmentTime.Add("08:30");
            listofAppointmentTime.Add("08:45");
            listofAppointmentTime.Add("09:00");
            listofAppointmentTime.Add("09:15");
            listofAppointmentTime.Add("09:30");
            listofAppointmentTime.Add("09:45");
            listofAppointmentTime.Add("10:00");
            listofAppointmentTime.Add("10:15");
            listofAppointmentTime.Add("10:30");
            listofAppointmentTime.Add("10:45");
            listofAppointmentTime.Add("11:00");
            listofAppointmentTime.Add("11:15");
            listofAppointmentTime.Add("11:30");
            listofAppointmentTime.Add("11:45");
            listofAppointmentTime.Add("12:00");
            listofAppointmentTime.Add("12:15");
            listofAppointmentTime.Add("12:30");
            listofAppointmentTime.Add("12:45");
            listofAppointmentTime.Add("13:00");
            listofAppointmentTime.Add("13:15");
            listofAppointmentTime.Add("13:30");
            listofAppointmentTime.Add("13:45");
            listofAppointmentTime.Add("14:00");
            listofAppointmentTime.Add("14:15");
            listofAppointmentTime.Add("14:30");
            listofAppointmentTime.Add("14:45");
            listofAppointmentTime.Add("15:00");
            listofAppointmentTime.Add("15:15");
            listofAppointmentTime.Add("15:30");
            listofAppointmentTime.Add("15:45");
            listofAppointmentTime.Add("16:00");
            listofAppointmentTime.Add("16:15");
            listofAppointmentTime.Add("16:30");
            listofAppointmentTime.Add("16:45");
        }
    }
}
