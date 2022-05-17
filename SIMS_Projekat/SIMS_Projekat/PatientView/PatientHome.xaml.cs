﻿using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for PatientHome.xaml
    /// </summary>
    public partial class PatientHome : Window
    {
        public Patient patient;
        private string nameSurname;
        static Timer timer;
        Page Homepage;
        public PatientHome(Patient p)
        {
            timer = new Timer(new TimerCallback(TherapyNotificationController.TickTimer), null, 60, 30000);
            InitializeComponent();
            patient = p;

            if (App.accountController.CheckIfItsNewMonth(patient))      //ako je novi mesec, promeni podatke u pacijentu i stavi broj otkazanih termina na 0
                ResetPatient();
                
            nameSurname = p.FirstName + " " + p.LastName;
            name_surname.Content = nameSurname;
            Homepage = new Homepage(patient);
            MainFrame.Content = Homepage;
        }
        private void ResetPatient()
        {
            Patient p = new Patient()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Allergens = patient.Allergens,
                Appointment = patient.Appointment,
                BloodType = patient.BloodType,
                DateOfBirth = patient.DateOfBirth,
                doctorLicenceNumber = patient.doctorLicenceNumber,
                Email = patient.Email,
                HealthInsuranceID = patient.HealthInsuranceID,
                Height = patient.Height,
                ID = patient.ID,
                IsUrgent = patient.IsUrgent,
                Jmbg = patient.Jmbg,
                MedicalRecord = patient.MedicalRecord,
                Password = patient.Password,
                Username = patient.Username,
                PhoneNumber = patient.PhoneNumber,
                Symptoms = patient.Symptoms,
                Weight = patient.Weight,
                year = DateTime.Now.Year,
                month = DateTime.Now.Month,
                numberOfCancelledAppointments = 0,
                MedicalRecordID = patient.MedicalRecordID
            };
            App.accountController.EditPatientAccount(p, patient.ID);
        }
        private void make_appointment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Appointments(MainFrame, patient);   
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            App.appointmentRepo.Serialize();
            App.accountRepository.Serialize();
            App.therapyNotificationRepository.Serialize();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            App.therapyNotificationController.DeleteActiveNotifications();
            App.appointmentRepo.Serialize();
            App.accountRepository.Serialize();
            App.therapyNotificationRepository.Serialize();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void homepage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Homepage(patient);
        }

        private void choose_doctor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ChooseDoctorPage(patient);
        }


    }
}
