using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Windows;

namespace SIMS_Projekat.SecretaryView
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
