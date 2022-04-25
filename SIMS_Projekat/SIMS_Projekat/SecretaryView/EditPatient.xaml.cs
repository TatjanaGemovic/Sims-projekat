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
    public partial class EditPatient : Window
    {
        public AccountController AccountController { get; set; }
        private string ID;
        private Patient patient;

        public EditPatient(AccountController accountController, Patient oldPatient)
        {
            InitializeComponent();
            AccountController = accountController;
            patient = oldPatient;

            FirstName.Text = patient.FirstName;
            LastName.Text = patient.LastName;
            Date.Text = patient.DateOfBirth.ToString();
            Jmbg.Text = patient.Jmbg;
            Email.Text = patient.Email;
            BloodType.SelectedIndex = (int)patient.BloodType;
            PhoneNumber.Text = patient.PhoneNumber;
            HealthInsuranceID.Text = patient.PhoneNumber;
            Height.Text = patient.Height.ToString();
            Weight.Text = patient.Weight.ToString();
            Username.Text = patient.Username;
            Password.Password = patient.Password;
            ID = patient.ID;
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
                Password = Password.Password      
            };
            AccountController.EditPatientAccount(newPatient, ID);

            AccountsView.Refresh();
            Close();
        }
    }
}
