using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.SecretaryView;
using System;
using System.Collections.Generic;
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

namespace SIMS_Projekat.PatientView
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        public AccountController AccountController { get; set; }
        public AddDoctor(AccountController accountController)
        {
            InitializeComponent();
            AccountController = accountController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newDoctor = new Doctor()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DateOfBirth = DateTime.Parse(Date.Text),
                Jmbg = Jmbg.Text,
                Email = Email.Text,
                PhoneNumber = PhoneNumber.Text,
                LicenceNumber = LicenceNumber.Text,
                Username = Username.Text,
                Password = Password.Password
            };
            AccountController.CreateDoctorAccount(newDoctor);
            AccountsView.AddDoctor(newDoctor);
            Close();
        }
    }
}
