using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
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
    /// Interaction logic for AddUrgentPatient.xaml
    /// </summary>
    public partial class AddUrgentPatient : Window
    {
        public AccountController AccountController { get; set; }
        public AddUrgentPatient(AccountController accountController)
        {
            InitializeComponent();
            AccountController = accountController;
        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            var newUrgentPatient = new Patient()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                BloodType = (BloodType)BloodType.SelectedIndex,
                DateOfBirth = new DateTime(1900, 1, 1),
                Height = Double.Parse(Height.Text),
                Weight = Double.Parse(Weight.Text),
                Jmbg = "",
                Email = "",
                PhoneNumber = "",
                HealthInsuranceID = "",
                Username = "",
                Password = "",
                Symptoms = Symptoms.Text,
                IsUrgent = true
            };

            AccountController.CreatePatientAccount(newUrgentPatient);
            SecretaryHome.AddPatient(newUrgentPatient);
            Close();
        }
    }
}
