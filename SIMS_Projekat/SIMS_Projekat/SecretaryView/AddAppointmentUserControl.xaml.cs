using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddPatientUserControl.xaml
    /// </summary>
    public partial class AddAppointmentUserControl : UserControl
    {
        private AccountController accountController;
        private RoomController roomController;
        private AppointmentController appointmentController;

        private DateTime selectedDate;
        private Room selectedRoom;
        private string selectedTime;

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<string> AvailableTimes { get; set; }
        public ObservableCollection<Doctor> AvailableDoctors { get; set; }
        public ObservableCollection<Patient> AvailablePatients { get; set; }

        private ContentControl contentControl;
        private UserControl appointmentUserControl;

        public AddAppointmentUserControl(RoomController roomController, AppointmentController appointmentController, AccountController accountController, ContentControl contentControl, UserControl appointmentUserControl, DateTime date, Room room)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            this.appointmentUserControl = appointmentUserControl;
            this.accountController = accountController;
            this.roomController = roomController;
            this.appointmentController = appointmentController;

            Rooms = new ObservableCollection<Room>(this.roomController.GetAvailableRooms());
            AvailableTimes = new ObservableCollection<string>(this.appointmentController.createAppointmentTime());
            AvailableDoctors = new ObservableCollection<Doctor>(accountController.GetAllDoctorAccounts());
            AvailablePatients = new ObservableCollection<Patient>(accountController.GetAllPatientAccounts());

            selectedDate = date;
            selectedRoom = room;
            AppointmentDatePicker.SelectedDate = date;
            RoomComboBox.SelectedItem = room;


        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan timeStart = TimeSpan.Parse(selectedTime);
            selectedDate = selectedDate.Add(timeStart);

            Doctor selectedDoctor = (Doctor)AvailableDoctorsListBox.SelectedItem;
            Patient selectedPatient = (Patient)AvailablePatientsListBox.SelectedItem;

            var newAppointment = new Appointment()
            {
                beginningDate = selectedDate,
                endDate = selectedDate.AddMinutes(15),
                doctor = selectedDoctor,
                patient = selectedPatient,
                room = selectedRoom,
                operation = false

            };
            appointmentController.AddAppointment(newAppointment);
            AppointmentsUserControl.AddAppointment(newAppointment);
            contentControl.Content = appointmentUserControl;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = appointmentUserControl;
        }


        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRoom = (Room)RoomComboBox.SelectedItem;
            UpdateAvailableTimes(selectedRoom.RoomID, selectedDate);
        }

        private void UpdateAvailableTimes(string roomID, DateTime date)
        {
            
            List<Appointment> allAppointments = appointmentController.GetAppointmentsByRoomIdAndDate(roomID, date);
            List<string> allTimes = appointmentController.createAppointmentTime();

            AvailableTimes.Clear();

            foreach(string time in allTimes)
            {
                AvailableTimes.Add(time);
            }
            
            foreach (Appointment appointment in allAppointments)
            {
                AvailableTimes.Remove(appointment.beginningDate.ToString("HH:mm"));
            }

            //lista se ponovo inicijalizuje -> mora se vratiti selektovana vrednost
            AvailableTimesComboBox.SelectedItem = selectedTime;

        }

        private void UpdateAvailableDoctors(DateTime date, string time)
        {
            string[] timeParts = time.Split(":");
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);

            DateTime selectedDateAndTime = new DateTime(selectedDate.Year, selectedDate.Month, 
                selectedDate.Day, hours, minutes, 0);
            List<Doctor> allDoctors = accountController.GetAllDoctorAccounts();

            foreach(Doctor doctor in allDoctors)
            {
                if (appointmentController.CheckIfDoctorIsAvailable(doctor, selectedDateAndTime))
                {
                    if(!AvailableDoctors.Contains(doctor))
                        AvailableDoctors.Add(doctor);

                }
                else
                {
                    AvailableDoctors.Remove(doctor);
                }
            }
        }

        private void UpdateAvailablePatients(DateTime date, string time)
        {
            string[] timeParts = time.Split(":");
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);

            DateTime selectedDateAndTime = new DateTime(selectedDate.Year, selectedDate.Month,
                selectedDate.Day, hours, minutes, 0);
            List<Patient> allPatients = accountController.GetAllPatientAccounts();

            foreach (Patient patient in allPatients)
            {
                if (appointmentController.CheckIfPatientIsAvailable(patient, selectedDateAndTime))
                {
                    if (!AvailablePatients.Contains(patient))
                        AvailablePatients.Add(patient);

                }
                else
                {
                    AvailablePatients.Remove(patient);
                }
            }
        }

        private void AvailableTimesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //kada se promeni soba ponovo se inicalizuje lista vremena
            //tada ce izabrano vreme biti null
            if (AvailableTimesComboBox.SelectedItem == null)
                return;

            selectedTime = (string)AvailableTimesComboBox.SelectedItem;
            UpdateAvailableDoctors(selectedDate, selectedTime);
            UpdateAvailablePatients(selectedDate, selectedTime);
        }
    }
}
