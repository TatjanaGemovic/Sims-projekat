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
    /// Interaction logic for AddUrgentPatientUserControl .xaml
    /// </summary>
    public partial class AddUrgentPatientUserControl : UserControl
    {
        public AccountController AccountController { get; set; }
        public RoomController RoomController { get; set; }
        public AppointmentController AppointmentController { get; set; }
        public ObservableCollection<Room> AvailableRooms { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }

        private ContentControl contentControl;
        private UserControl accountsView;
        private RadioButton accountsRadioButton;

        public AddUrgentPatientUserControl(AccountController accountController, RoomController roomController,
            AppointmentController appointmentController, ContentControl contentControl, UserControl accountsView,
            RadioButton radioButton)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            this.accountsView = accountsView;

            RoomController = roomController;
            AccountController = accountController;
            AppointmentController = appointmentController;

            accountsRadioButton = radioButton;

            AvailableRooms = new ObservableCollection<Room>(GetRooms().OrderBy(room => room.RoomNumber));
            Patients = new ObservableCollection<Patient>(AccountController.GetAllPatientAccounts());
        }

        private List<Room> GetRooms()
        {
            List<Room> rooms = RoomController.GetAvailableRooms(CreateStartTimeForCurrentAppointment());
            if(rooms.Count <= 0)
            {
                rooms.Add(RoomController.GetRoomsByType(RoomType.examRoom)[0]);
            }
            return rooms;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Patient patient;
            if (NewPatientExpander.IsExpanded)
            {
                patient = CreateNewPatient();
            }
            else if (ExistingPatientExpander.IsExpanded)
            {
                patient = ExistingPatientsListBox.SelectedItem as Patient;
            }
            else
            {
                MessageBox.Show("Morate izabrati pacijenta iz liste postojecih ili napraviti novi nalog");
                return;
            }

            CreateNewAppointment(patient);

            contentControl.Content = accountsView;
            accountsRadioButton.IsChecked = true;
        }

        private Patient CreateNewPatient()
        {
            var newPatient = new Patient()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DateOfBirth = DateTime.Parse(Date.Text),
                BloodType = (BloodType)BloodType.SelectedIndex,
                Symptoms = Symptoms.Text,
                IsUrgent = true,
                MedicalRecord = App.medRecordRepository.CreateMedicalRecord(new MedicalRecord()),
                Allergens = new List<Allergen>(),
                month = DateTime.Now.Month,
                year = DateTime.Now.Year,
                numberOfCancelledAppointments = 0
            };
            AccountController.CreatePatientAccount(newPatient);
            AccountsView.AddPatient(newPatient);

            return newPatient;
        }
        
        private void CreateNewAppointment(Patient patient)
        {
            DateTime dateTime = CreateStartTimeForCurrentAppointment();
            List<Doctor> availableDoctors = AccountController.GetAvailableDoctors(dateTime);
            while(availableDoctors.Count <= 0)
            {
                RescheduleDoctorAppointment(AccountController.GetAllDoctorAccounts()[0]);
                availableDoctors = AccountController.GetAvailableDoctors(dateTime);
            }

            var newAppointment = new Appointment()
            {
                beginningDate = dateTime,
                endDate = dateTime.AddMinutes(15),
                doctor = availableDoctors[0],
                patient = patient,
                room = (Room)RoomComboBox.SelectedItem,
                operation = false,
                isDelayed = false,
                isScheduledByPatient = false

            };
            AppointmentController.AddAppointment(newAppointment);
            AppointmentsUserControl.AddAppointment(newAppointment);
        }

        private void RescheduleDoctorAppointment(Doctor doctor)
        {
            AppointmentController.RescheduleCurrentAppointmentForDoctor(doctor, CreateStartTimeForCurrentAppointment());
        }

        private DateTime CreateStartTimeForCurrentAppointment()
        {
            DateTime startDateTime = DateTime.Now;
            int minutes = GetCurrentAppointmentStartMinute(startDateTime.Minute);
            
            return new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 
                startDateTime.Hour, minutes, 0);
        }

        private int GetCurrentAppointmentStartMinute(int currentMinute)
        {
            return currentMinute / 15 * 15;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            accountsRadioButton.IsChecked = true;
            contentControl.Content = accountsView;
        }

        private void ExistingPatientExpander_Expanded(object sender, RoutedEventArgs e)
        {
            NewPatientExpander.IsExpanded = false;
        }

        private void NewPatientExpander_Expanded(object sender, RoutedEventArgs e)
        {
            ExistingPatientExpander.IsExpanded = false;
        }

    }
}
