using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private static string ACCOUNTS_CSV = "accounts.txt";
        public App()
        {
            var accountRepository = new AccountRepository(ACCOUNTS_CSV);
            var accountService = new AccountService()
            {
                AccountRepository = accountRepository
            };
            var accountController = new AccountController()
            {
                AccountService = accountService
            };

            Patient patient1 = new Patient()
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
            };

            Patient patient2 = new Patient()
            {
                FirstName = "Jovan",
                LastName = "Jovanovic",
                DateOfBirth = new DateTime(2030, 5, 5),
                Jmbg = "0505030235524",
                PhoneNumber = "+38195252351",
                Email = "email@outlook.com",
                Username = "jjovan",
                Password = "54321",
                ID = "002",
                HealthInsuranceID = "41234123412",
                BloodType = BloodType.oPositive,
                Height = 170,
                Weight = 70
            };

            accountController.CreatePatientAccount(patient1);
            accountController.CreatePatientAccount(patient2);



            accountRepository.Serialize();
            patient1.FirstName = "Pera";
            Trace.WriteLine(accountRepository.GetAllPatientAccounts().ElementAt(0).FirstName);
            accountRepository.Deserialize();
            Trace.WriteLine(accountRepository.GetAllPatientAccounts().ElementAt(0).FirstName);
        }
    }
}
