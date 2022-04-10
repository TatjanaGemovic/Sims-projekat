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

        private static string sOperations_CSV = @".\..\..\..\Resources\sOperations.txt";
        public static ScheduledOperationRepository scheduledOperationRepository;
        public static ScheduledOperationController ScheduledOperationController;
        private static string APPOINTMENT_FILE = @".\..\..\..\Resources\appointment.txt";
        public static AppointmentRepository appointmentRepo;

        public static AppointmentController appointmentController;
        public App()
        {
            scheduledOperationRepository = new ScheduledOperationRepository(sOperations_CSV);

            ScheduledOperationService ScheduledOperationService = new ScheduledOperationService(scheduledOperationRepository);

            ScheduledOperationController = new ScheduledOperationController(ScheduledOperationService);

            appointmentRepo = new AppointmentRepository(APPOINTMENT_FILE);
            AppointmentService appointmentService = new AppointmentService()
            {
                appointmentRepository = appointmentRepo
            };
            appointmentController = new AppointmentController()
            {
                appointmentService = appointmentService
            };

            ScheduledOperation so1 = new ScheduledOperation()
            {
                Start = new DateTime(2022, 8, 18, 13, 30, 30),
                End = new DateTime(2022, 8, 18, 14, 30, 30),
                OperationType = "cardiology",
                OperationID = 12345,
                Doctor = new Doctor(),
                Room = new Room(),
                Patient = new Patient()
                {
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    DateOfBirth = new DateTime(2020, 10, 10),
                    Jmbg = "10100204240124",
                    PhoneNumber = "+36161163343",
                    Email = "email@gmail.com",
                    Username = "ppera",
                    Password = "12345",
                    ID = "001",
                    HealthInsuranceID = "1524159823",
                    BloodType = BloodType.oPositive,
                    Height = 180,
                    Weight = 80
                },
            };
             

            Doctor doctor = new Doctor()
            {
                FirstName = "Pera",
                LastName = "Peric",
                Email = "pera@gmail.com",
                Jmbg = "111122440",
                Username = "pera",
                Password = "pera123",
                PhoneNumber = "0641111111",
                DateOfBirth = new DateTime(1994, 5, 15),
                ID = "10",
                LicenceNumber = "154812014"
            };
            Patient patient1 = new Patient()
            {
                PatientID = "500",
                FirstName = "Jovan",
                LastName = "Jovanovic",
                Email = "jovan@gmail.com",
                Jmbg = "512155120",
                Username = "joca",
                Password = "joca123",
                PhoneNumber = "0645554442",
                DateOfBirth = new DateTime(2000, 10, 15),
                BloodType = BloodType.aPositive,
                Height = 178.0,
                Weight = 80.0,
                ID = "540",
                HealthInsuranceID = "005426"
            };
            Patient patient2 = new Patient()
            {

                PatientID = "300",
                FirstName = "Ivana",
                LastName = "Ivanovic",
                Email = "ivana@gmail.com",
                Jmbg = "512155120",
                Username = "icka",
                Password = "icka123",
                PhoneNumber = "0645554442",
                DateOfBirth = new DateTime(2000, 10, 15),
                BloodType = BloodType.aPositive,
                Height = 178.0,
                Weight = 80.0,
                ID = "340",
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

            appointmentRepo.Deserialize();
            scheduledOperationRepository.Deserialize();
           
        }
    }
}
