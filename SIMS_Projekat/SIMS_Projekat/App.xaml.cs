using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using SIMS_Projekat.Service;
using System;
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
        public App()
        {
            scheduledOperationRepository = new ScheduledOperationRepository(sOperations_CSV);

            ScheduledOperationService ScheduledOperationService = new ScheduledOperationService(scheduledOperationRepository);

            ScheduledOperationController = new ScheduledOperationController(ScheduledOperationService);


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
            //ScheduledOperationController.ScheduleOperation(so1);
            //scheduledOperationRepository.Serialize();
            scheduledOperationRepository.Deserialize();
        }
    }
}
