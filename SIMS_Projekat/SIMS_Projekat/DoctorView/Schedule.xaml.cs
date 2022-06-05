using SIMS_Projekat.DTO;
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
    /// Interaction logic for Schedule.xaml
    /// </summary>
    public partial class Schedule : Page
    {
        private Frame Frame;
        private Doctor doctor;
        private Doctor selectedDoctor;
        private Patient patient;
        private Appointment appointment;
        BindingList<String> appointmentType;
        BindingList<String> doctors1;
        BindingList<String> rooms;
        BindingList<String> listofTakenAppointmentTime;
        BindingList<String> listofAppointmentTime;
        private List<Doctor> doctors;
        Room selectedRoom;
        bool op;
        String selectedDate1;
        public Schedule(Frame frame, Doctor doctor1, Patient patient1, Appointment appointment1)
        {
            Frame = frame;
            doctor = doctor1;
            patient = patient1;
            appointment = appointment1;
            InitializeComponent();

            InitializeComboBox1();
            InitializeComboBox2();
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
            doctors1 = new BindingList<String>();
            doctors = App.accountController.GetAllDoctorAccounts();

            foreach (Doctor d in doctors)
            {
                doctors1.Add(d.FirstName + " " + d.LastName);
            }
            Ime_lekara.ItemsSource = doctors1;
        }

        private void InitializeComboBox3()
        {
            rooms = new BindingList<String>();
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.pRoomType == RoomType.examRoom)
                {
                    rooms.Add(r.RoomNumber.ToString());
                }

            }
            Ime_sobe.ItemsSource = rooms;
        }
        private void InitializeComboBox4()
        {
            rooms = new BindingList<String>();
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.pRoomType == RoomType.operatingRoom)
                {
                    rooms.Add(r.RoomNumber.ToString());
                }

            }
            Ime_sobe.ItemsSource = rooms;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ExaminationInfo(Frame, appointment, doctor);
        }

        private void Tip_operacije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String tip = Tip_operacije.SelectedItem.ToString();
            if (tip.Equals("Pregled"))
            {
                op = false;
                InitializeComboBox3();
            }
            else
            {
                op = true;
                InitializeComboBox4();
            }
            Ime_sobe.IsHitTestVisible = true;
            Ime_sobe.IsEnabled = true;
        }

        private void Ime_lekara_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String doctor = Ime_lekara.SelectedItem.ToString();
            foreach (Doctor d in App.accountController.GetAllDoctorAccounts())
            {
                string str = d.FirstName + " " + d.LastName;
                if (str.Equals(doctor))
                {
                    selectedDoctor = d;
                }
            }
        }

        private void Ime_sobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String id = Ime_sobe.SelectedItem.ToString();
            foreach (Room r in App.roomController.GetRooms())
            {
                if (r.RoomNumber == int.Parse(id))
                {
                    selectedRoom = r;
                }
            }
        }

        private void Vreme_pocetka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Ime_lekara.SelectedItem != null && Tip_operacije.SelectedItem != null && Ime_sobe.SelectedItem != null && Datum_pocetka.SelectedDate != null)
            {
                Dodaj_operaciju.IsEnabled = true;
            }
        }

        private void Datum_pocetka_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate1 = Datum_pocetka.SelectedDate.ToString();
            String[] datePart = selectedDate1.Split(" ");
            selectedDate1 = datePart[0];

            if (Ime_lekara.SelectedItem != null && Tip_operacije.SelectedItem != null && Ime_sobe.SelectedItem != null)
            {
                Vreme_pocetka.IsHitTestVisible = true;
                Vreme_pocetka.IsEnabled = true;
                InitializeListOfAppointments();
            }
        }
        private void InitializeListOfAppointments()
        {
            List<String> list = new List<string>();

            AppointmentServiceDTO dto = new AppointmentServiceDTO()
            {
                doctor = selectedDoctor,
                date = selectedDate1,
                patient = patient,
                room = selectedRoom
            };

            list = App.appointmentController.GetAvailableAppointmentsForDoctor(dto);

            listofAppointmentTime = new BindingList<String>();
            listofTakenAppointmentTime = new BindingList<String>(list);

            listofAppointmentTime.AllowNew = true;
            listofAppointmentTime.AllowRemove = true;

            listofAppointmentTime.RaiseListChangedEvents = true;

            listofAppointmentTime.AllowEdit = false;
            CreateList();

            foreach (string time in listofTakenAppointmentTime)
            {
                listofAppointmentTime.Remove(time);
            }
            Vreme_pocetka.ItemsSource = listofAppointmentTime;
        }

        private void Dodaj_operaciju_Click(object sender, RoutedEventArgs e)
        {
            string dateFromPage = selectedDate1.ToString();
            DateTime start = DateTime.Parse(dateFromPage);
            DateTime startDate = start.Date;

            string timeFromPage = this.Vreme_pocetka.SelectedItem.ToString();
            TimeSpan timeStart = TimeSpan.Parse(timeFromPage);
            startDate = startDate.Add(timeStart);

            Appointment appointment = new Appointment()
            {
                beginningDate = startDate,
                endDate = startDate.AddMinutes(15),
                operation = op,
                room = selectedRoom,
                doctor = selectedDoctor,
                patient = patient,
                isDelayedByPatient = false,
                isScheduledByPatient = false
            };

            App.appointmentController.AddAppointment(appointment);
            App.appointmentRepo.Serialize();
            Frame.Content = new ExaminationInfo(Frame, appointment, doctor);;
        }

        private void Vreme_pocetka_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            Vreme_pocetka.ItemsSource = listofAppointmentTime;
        }

        private void CreateList()
        {
            listofAppointmentTime = new BindingList<String>(App.appointmentController.CreateAppointmentTime());
        }
    }
}
