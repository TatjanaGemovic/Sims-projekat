﻿using SIMS_Projekat.Controller;
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
        public ObservableCollection<Appointment> AppointmentsForRescheduling { get; set; }

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

            if(AvailableRooms.Count == 0 || 
                AccountController.GetAvailableDoctors(CreateStartTimeForCurrentAppointment()).Count == 0)
            {
                FindFirstFreeRoom();
                ShowAppointmentsForRescheduling();
            }

            Patients = new ObservableCollection<Patient>(AccountController.GetAllPatientAccounts());
        }

        private void FindFirstFreeRoom()
        {
            DateTime dateTime = CreateStartTimeForCurrentAppointment().AddMinutes(15);
            DateTime tommorow = DateTime.Today.AddDays(1);
            while(dateTime < tommorow)
            {
                List<Room> rooms = RoomController.GetAvailableRooms(dateTime);
                if(rooms.Count != 0)
                {
                    AppointmentController.AddAppointment(new Appointment
                    {
                        beginningDate = dateTime,
                        endDate = dateTime.AddMinutes(15),
                        room = rooms[0],
                        roomID = rooms[0].RoomID
                    });
                    return;
                }
                dateTime = dateTime.AddMinutes(15);


            }
        }

        private List<Room> GetRooms()
        {
            return RoomController.GetAvailableRooms(CreateStartTimeForCurrentAppointment());
        }

        private void ShowAppointmentsForRescheduling()
        {
            RoomStackPanel.Visibility = Visibility.Hidden;
            CancelAppointmentStackPanel.Visibility = Visibility.Visible;
            AppointmentsForRescheduling = new ObservableCollection<Appointment>(GetTodayAppointments());
        }
        private List<Appointment> GetTodayAppointments() 
        {
            return AppointmentController.GetAllAppointmentForToday();
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
                currentMonthUsableForCancellingAppointmentsByPatient = DateTime.Now.Month,
                currentYearUsableForCancellingAppointmentsByPatient = DateTime.Now.Year,
                numberOfCancelledAppointmentsByPatientMonthly = 0
            };
            AccountController.CreatePatientAccount(newPatient);
            AccountsView.AddPatient(newPatient);

            return newPatient;
        }
        
        private void CreateNewAppointment(Patient patient)
        {
            Appointment newAppointment;

            if(CancelAppointmentStackPanel.Visibility == Visibility.Visible)
            {
                Appointment selectedAppointment = (Appointment)AppointmentsListBox.SelectedItem;
                AppointmentController.DeleteAppointment(selectedAppointment);

                DateTime dateTime = CreateStartTimeForCurrentAppointment();

                newAppointment = new Appointment()
                {
                    beginningDate = selectedAppointment.beginningDate,
                    endDate = selectedAppointment.endDate,
                    doctor = AccountController.GetAvailableDoctors(dateTime)[0],
                    patient = patient,
                    room = selectedAppointment.room,
                    operation = false,
                    isDelayedByPatient = false,
                    isScheduledByPatient = false
                };
            }
            else
            {
                DateTime dateTime = CreateStartTimeForCurrentAppointment();

                newAppointment = new Appointment()
                {
                    beginningDate = dateTime,
                    endDate = dateTime.AddMinutes(15),
                    doctor = AccountController.GetAvailableDoctors(dateTime)[0],
                    patient = patient,
                    room = (Room)RoomComboBox.SelectedItem,
                    operation = false,
                    isDelayedByPatient = false,
                    isScheduledByPatient = false

                };
            }
            
            AppointmentController.AddAppointment(newAppointment);
            AppointmentsUserControl.AddAppointment(newAppointment);
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
