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

        private static string APPOINTMENT_FILE = @".\..\..\..\Resources\appointment.txt";
        public static AppointmentRepository appointmentRepo;

        private static string PATIENTS_CSV = @".\..\..\..\Resources\patients.txt";
        private static string DOCTORS_CSV = @".\..\..\..\Resources\doctors.txt";
        public static AccountRepository accountRepository;
        public static AccountService accountService;
        public static AccountController accountController;

        public static RoomController roomController;
        public static AppointmentController appointmentController;
        public App()
        {
           
            roomController = new RoomController();
            appointmentRepo = new AppointmentRepository(APPOINTMENT_FILE);
            AppointmentService appointmentService = new AppointmentService()
            {
                appointmentRepository = appointmentRepo
            };
            appointmentController = new AppointmentController()
            {
                appointmentService = appointmentService
            };

            accountRepository = new AccountRepository(PATIENTS_CSV, DOCTORS_CSV);
            accountService = new AccountService()
            {
                AccountRepository = accountRepository
            };
            accountController = new AccountController()
            {
                AccountService = accountService
            };

            accountRepository.Deserialize();
            roomController.Deserialize();
            appointmentRepo.Deserialize();
           

        }
    }
}
