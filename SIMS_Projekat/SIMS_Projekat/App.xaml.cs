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
using SIMS_Projekat.Controller;
using SIMS_Projekat.Repository;
using SIMS_Projekat.Service;
using SIMS_Projekat.Model;
using System.Diagnostics;


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
        //private static string ROOM_CSV = @".\..\..\..\Resources\rooms.txt";

        //private static string PATIENTS_CSV = @".\..\..\..\Resources\patients.txt";
        //private static string DOCTORS_CSV = @".\..\..\..\Resources\doctors.txt";
        //public static AccountRepository accountRepository;
        //public static AccountService accountService;
        //public static AccountController accountController;

        //public static RoomRepository roomRepository;
        //public static RoomService roomService;
        //public static RoomController roomController;

        public static AppointmentController appointmentController;
        public App()
        {
            scheduledOperationRepository = new ScheduledOperationRepository(sOperations_CSV);

            ScheduledOperationService ScheduledOperationService = new ScheduledOperationService(scheduledOperationRepository);

            ScheduledOperationController = new ScheduledOperationController(ScheduledOperationService);

            //roomRepository = new RoomRepository(ROOM_CSV);
            //roomService = new RoomService()
            //{
            //    roomRepository = roomRepository
            //};
            //roomController = new RoomController()
            //{
            //    roomService = roomService
            //};

            appointmentRepo = new AppointmentRepository(APPOINTMENT_FILE);
            AppointmentService appointmentService = new AppointmentService()
            {
                appointmentRepository = appointmentRepo
            };
            appointmentController = new AppointmentController()
            {
                appointmentService = appointmentService
            };

            //accountRepository = new AccountRepository(PATIENTS_CSV, DOCTORS_CSV);
            //accountService = new AccountService()
            //{
            //    AccountRepository = accountRepository
            //};
            //accountController = new AccountController()
            //{
            //    AccountService = accountService
            //};

           

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
                    BloodType = BloodType.O_Positive,
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
                ID = "500",
                FirstName = "Jovan",
                LastName = "Jovanovic",
                Email = "jovan@gmail.com",
                Jmbg = "512155120",
                Username = "joca",
                Password = "joca123",
                PhoneNumber = "0645554442",
                DateOfBirth = new DateTime(2000, 10, 15),
                BloodType = BloodType.A_Positive,
                Height = 178.0,
                Weight = 80.0,
                HealthInsuranceID = "005426"
            };
            Patient patient2 = new Patient()
            {

                ID = "300",
                FirstName = "Ivana",
                LastName = "Ivanovic",
                Email = "ivana@gmail.com",
                Jmbg = "512155120",
                Username = "icka",
                Password = "icka123",
                PhoneNumber = "0645554442",
                DateOfBirth = new DateTime(2000, 10, 15),
                BloodType = BloodType.A_Positive,
                Height = 178.0,
                Weight = 80.0,
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

            /*Appointment newAppointment1 = new Appointment()
            {
                beginningDate = new DateTime(2022, 04, 30, 9, 0, 00),
                endDate = new DateTime(2022, 04, 30, 9, 0, 00),
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
            */
            //accountRepository.Deserialize();
            //roomRepository.Deserialize();
            appointmentRepo.Deserialize();
            scheduledOperationRepository.Deserialize();


            //foreach (Appointment appointment in appointmentRepo.GetAllAppointments())
            //{

            //    appointment.patient = accountRepository.GetPatientAccountByID(appointment.patientID) as Patient;
            //    appointment.doctor = accountRepository.GetDoctorAccountByLicenceNumber(appointment.licenceNumber) as Doctor;

            //    appointment.room = roomRepository.GetRoomByID(appointment.roomID);

            //    //appointment.room = new Room
            //    //{
            //    //    Available = false,
            //    //    Floor = 2,
            //    //    RoomNumber = 2,
            //    //    Type = RoomType.examRoom,
            //    //    RoomID = "555"
            //    //};

            //    //id = appointment.appointmentID;
            //}


        }
    }
}
