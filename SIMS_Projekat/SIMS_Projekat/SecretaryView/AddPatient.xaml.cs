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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public AccountController AccountController { get; set; }
        public AddPatient(AccountController accountController)
        {
            InitializeComponent();
            AccountController = accountController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newPatient = new Patient()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DateOfBirth = DateTime.Parse(Date.Text),
                Jmbg = Jmbg.Text,
                Email = Email.Text,
                BloodType = (BloodType)BloodType.SelectedIndex,
                PhoneNumber = PhoneNumber.Text,
                HealthInsuranceID = HealthInsuranceID.Text,
                Height = Double.Parse(Height.Text),
                Weight = Double.Parse(Weight.Text),
                Username = Username.Text,
                Password = Password.Password,
                Symptoms = "N/A",
                IsUrgent = false,
                MedicalRecord = App.medRecordRepository.CreateMedicalRecord(new MedicalRecord())
            };
            AccountController.CreatePatientAccount(newPatient);
            AccountsView.AddPatient(newPatient);
            Close();
        }
    }
}
