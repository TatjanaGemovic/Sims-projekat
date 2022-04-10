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
            var newUrgentPatient = new UrgentPatient()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                BloodType = (BloodType)BloodType.SelectedIndex,
                Height = Double.Parse(Height.Text),
                Weight = Double.Parse(Weight.Text),
                Informations = Informations.Text
            };
            AccountController.CreateUrgentPatientAccount(newUrgentPatient);
            UrgentPatientView.AddUrgentPatient(newUrgentPatient);
            Close();
        }
    }
}
