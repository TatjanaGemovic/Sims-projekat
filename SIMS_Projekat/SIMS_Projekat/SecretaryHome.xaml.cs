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
        private readonly AccountsView accountsView;
        private readonly AppointmentsView appointmentsView;

        public SecretaryHome(AccountRepository accountRepository, AccountController accountController)
        {
            InitializeComponent();
            accountsView = new AccountsView(accountRepository, accountController);
            appointmentsView = new AppointmentsView();
            ContentControl.Content = accountsView;
            Accounts_RadioButton.IsChecked = true;
        }

        private void LogOut_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            AccountsView.Serialize();
        }

        private void Accounts_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = accountsView;
        }
        private void Appointments_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = appointmentsView;
        }
    }
}
