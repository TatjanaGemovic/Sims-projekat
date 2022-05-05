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
    public partial class EditAppointmentUserControl : UserControl
    {
        private AccountController accountController;
        private RoomController roomController;
        private AppointmentController appointmentController;

        private DateTime selectedDate;
        private Room selectedRoom;
        private string selectedTime;
        private Appointment selectedAppointment;

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<string> AvailableTimes { get; set; }
        public ObservableCollection<Doctor> AvailableDoctors { get; set; }
        public ObservableCollection<Patient> AvailablePatients { get; set; }

        private ContentControl contentControl;
        private UserControl appointmentUserControl;

        public EditAppointmentUserControl(RoomController roomController, AppointmentController appointmentController, AccountController accountController, ContentControl contentControl, UserControl appointmentUserControl, Appointment selectedAppointment)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            this.appointmentUserControl = appointmentUserControl;
            this.accountController = accountController;
            this.roomController = roomController;
            this.appointmentController = appointmentController;
            this.selectedAppointment = selectedAppointment;

            Rooms = new ObservableCollection<Room>(this.roomController.GetAvailableNotMeetingRooms());
            AvailableTimes = new ObservableCollection<string>(this.appointmentController.createAppointmentTime());
            AvailableDoctors = new ObservableCollection<Doctor>(accountController.GetAllDoctorAccounts());
            AvailablePatients = new ObservableCollection<Patient>(accountController.GetAllPatientAccounts());

            selectedDate = selectedAppointment.beginningDate;
            selectedRoom = selectedAppointment.room;
            AppointmentDatePicker.SelectedDate = selectedAppointment.beginningDate;
            RoomComboBox.SelectedItem = selectedAppointment.room;
            PatientsSearchField.Text = selectedAppointment.patient.FirstName + " " + selectedAppointment.patient.LastName;
            DoctorSearchField.Text = selectedAppointment.doctor.FirstName + " " + selectedAppointment.doctor.LastName;
            selectedTime = selectedAppointment.beginningDate.ToString("HH:mm");
            AvailableTimesComboBox.SelectedItem = selectedAppointment.beginningDate.ToString("HH:mm");

        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string[] timeParts = selectedTime.Split(":");
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);
            int endMinutes = (minutes == 45) ? 0 : minutes+15;
            int endHours = (minutes == 45) ? ++hours : hours;

            Doctor selectedDoctor;
            if(AvailableDoctorsListBox.SelectedItem == null)
            {
                selectedDoctor = selectedAppointment.doctor;
            }
            else
            {
                selectedDoctor = (Doctor)AvailableDoctorsListBox.SelectedItem;
            }
            Patient selectedPatient;
            if (AvailableDoctorsListBox.SelectedItem == null)
            {
                selectedPatient = selectedAppointment.patient;
            }
            else
            {
                selectedPatient = (Patient)AvailablePatientsListBox.SelectedItem;
            }

            var newAppointment = new Appointment()
            {
                appointmentID = selectedAppointment.appointmentID,
                beginningDate = new DateTime(selectedDate.Year, selectedDate.Month,
                    selectedDate.Day, hours, minutes, 0),
                endDate = new DateTime(selectedDate.Year, selectedDate.Month,
                    selectedDate.Day, endHours, endMinutes, 0),
                doctor = selectedDoctor,
                patient = selectedPatient,
                room = selectedRoom,
                operation = false

            };
            appointmentController.SetAppointment(newAppointment);
            AppointmentsUserControl.SortDataGrid();
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

        private void AvailablePatientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PatientsSearchField.Text = "";
        }

        private void AvailableDoctorsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorSearchField.Text = "";
        }


    }
}
