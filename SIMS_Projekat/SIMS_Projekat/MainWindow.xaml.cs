using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string ACCOUNTS_CSV = "accounts.txt";
        private AccountRepository accountRepository;
        private AccountService accountService;
        private AccountController accountController;
        public MainWindow()
        {
            InitializeComponent();
            accountRepository = new AccountRepository(ACCOUNTS_CSV);
            accountService = new AccountService()
            {
                AccountRepository = accountRepository
            };
            accountController = new AccountController()
            {
                AccountService = accountService
            };
            accountRepository.Deserialize();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorHome doctorHomePage = new DoctorHome();
            this.Close();
            doctorHomePage.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManagerHome managerHomePage = new ManagerHome();
            this.Close();
            managerHomePage.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PatientHome patientHomePage = new PatientHome();
            this.Close();
            patientHomePage.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SecretaryHome secretaryHomePage = new SecretaryHome(accountRepository, accountController);
            this.Close();
            secretaryHomePage.Show();
        }

    }
}
