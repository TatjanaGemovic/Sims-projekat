using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string APPOINTMENT_FILE = "appointment.txt";
        public static AppointmentRepository appointmentRepo;

        public static AppointmentController appointmentController;
        public App()
        {
      
            
            
            appointmentRepo = new AppointmentRepository(APPOINTMENT_FILE);
            AppointmentService appointmentService = new AppointmentService()
            {
                appointmentRepository = appointmentRepo
            };
           appointmentController = new AppointmentController()
            {
                appointmentService = appointmentService
            };

            Doctor doctor = new Doctor()
            {
                FirstName = "Pera",
                LastName = "Peric",
                Email = "pera@gmail.com",
                Jmbg = "111122440",
                username = "pera",
                password = "pera123",
                PhoneNumber = "0641111111",
                DateOfBirth = new DateTime(1994, 5, 15),
                id = "10",
                LicenceNumber = "154812014"
            };
            Patient patient1 = new Patient()
            {
                PatientID = "500",
                FirstName = "Jovan",
                LastName = "Jovanovic",
                Email = "jovan@gmail.com",
                Jmbg = "512155120",
                username = "joca",
                password = "joca123",
                PhoneNumber = "0645554442",
                DateOfBirth = new DateTime(2000, 10, 15),
                BloodType = BloodType.aPositive,
                Height = 178.0,
                Weight = 80.0,
                id = "540",
                HealthInsuranceID = "005426"
            };
            Patient patient2 = new Patient()
            {

                PatientID = "300",
                FirstName = "Ivana",
                LastName = "Ivanovic",
                Email = "ivana@gmail.com",
                Jmbg = "512155120",
                username = "icka",
                password = "icka123",
                PhoneNumber = "0645554442",
                DateOfBirth = new DateTime(2000, 10, 15),
                BloodType = BloodType.aPositive,
                Height = 178.0,
                Weight = 80.0,
                id = "340",
                HealthInsuranceID = "005426"
            };

            Room room = new Room()
            { 
                RoomID = "5",
                Floor = 1,
                Type = RoomType.examRoom,
                RoomNumber = 4,
                Available = false,
            };

            Appointment newAppointment1 = new Appointment()
            {
                date = new DateTime(2022, 04, 30, 9, 0, 00),
                doctor = doctor,
                patient = patient1,
                room = room
            };

            Appointment newAppointment2 = new Appointment()
            { 
                date = new DateTime(2022, 04, 30, 10, 30, 00),
                doctor = doctor,
                patient = patient2,
                room = room
            };
            //Appointment NewAppointment2 = new Appointment()
            //{
            //    AppointmentID = 2,
            //    Date = new DateTime(2022, 05, 10, 16, 45, 00),
            //    Doctor = null,
            //    Patient = null,
            //    Room = null
            //};

            //appointmentController.AddAppointment(newAppointment1);
            //appointmentController.AddAppointment(newAppointment2);
            //appointmentRepo.Serialize();

            //newAppointment1.appointmentID = 152;
            //Trace.WriteLine(appointmentRepo.GetAllAppointments().ElementAt(0).appointmentID);

            appointmentRepo.Deserialize();
            //Trace.WriteLine(appointmentRepo.GetAllAppointments().ElementAt(0).appointmentID);

            //appointmentRepo.Serialize();
        }
    }
}
