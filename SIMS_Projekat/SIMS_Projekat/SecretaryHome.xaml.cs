using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView;
using SIMS_Projekat.Repository;
using SIMS_Projekat.SecretaryView;
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
using System.Windows.Shapes;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for SecretaryHome.xaml
    /// </summary>
    public partial class SecretaryHome : Window
    {
        public object CurrentView { get; set; }

        public SecretaryHome(AccountRepository accountRepository, AccountController accountController)
        {
            InitializeComponent();
            AccountsView accountsView = new AccountsView(accountRepository, accountController);
            CurrentView = accountsView;
            Content.Content = accountsView;

        }

        

        private void LogOut_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            //AccountRepository.Serialize();
        }
    }
}
