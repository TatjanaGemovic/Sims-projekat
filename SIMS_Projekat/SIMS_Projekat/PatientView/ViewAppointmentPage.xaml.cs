﻿using SIMS_Projekat.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for ViewAppointmentPage.xaml
    /// </summary>
    public partial class ViewAppointmentPage : Page
    {
        Patient patient;
        Frame mainFrame;
        Appointment appointment;
        public ViewAppointmentPage(Frame frame, int appointmentID, Patient p)
        {
            InitializeComponent();
            mainFrame = frame;
            patient = p;
            appointment = App.appointmentController.GetAppointmentByID(appointmentID);

            FillLabels();     
            ButtonControl();        
        }
        public void FillLabels()
        {
            Doctor d = App.accountController.GetDoctorAccountByLicenceNumber(appointment.doctor.LicenceNumber) as Doctor;
            chosenDoctor.Content = d.FirstName + " " + d.LastName;

            dateField.Content = appointment.beginningDate.Date.ToString("dd.MM.yyyy.");
            timeField.Content = appointment.beginningDate.TimeOfDay.ToString(@"hh\:mm");

            Room r = App.roomController.GetRoomByID(appointment.room.RoomID);
            roomField.Content = "Sprat " + r.Floor + ", broj " + r.RoomNumber;
        }
        public void ButtonControl()
        {
            if (appointment.operation)
            {
                isOperationField.Content = "Operacija";
                changeButton.IsEnabled = false;
            }
            else
            {
                if (appointment.isDelayedByPatient)
                {
                    changeButton.IsEnabled = false;
                }
                isOperationField.Content = "Pregled";
            }

            if (patient.numberOfCancelledAppointmentsByPatientMonthly == 2)
            {
                deleteButton.IsEnabled = false;
            }
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Jeste li sigurni da zelite da otkazete odabrani termin?",
            "Otkazivanje termina", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                patient.numberOfCancelledAppointmentsByPatientMonthly++;
                App.appointmentController.DeleteAppointment(appointment);

                Appointments Appointments = new Appointments(mainFrame, patient);
                mainFrame.Content = Appointments;
            }
            
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Appointments Appointments = new Appointments(mainFrame, patient);
            mainFrame.Content = Appointments;
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            ChangeAppointmentPage changeAppointmentPage = new ChangeAppointmentPage(mainFrame, appointment.appointmentID, patient);
            mainFrame.Content = changeAppointmentPage;
        }
    }
}
