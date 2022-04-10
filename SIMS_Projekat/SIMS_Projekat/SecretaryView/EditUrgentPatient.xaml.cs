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
    /// Interaction logic for EditUrgentPatient.xaml
    /// </summary>
    public partial class EditUrgentPatient : Window
    {
        public AccountController AccountController { get; set; }
        private string ID;
        private UrgentPatient patient;
        public EditUrgentPatient(AccountController accountController, UrgentPatient oldUrgentPatient)
        {
            InitializeComponent();
            AccountController = accountController;
            patient = oldUrgentPatient;

            FirstName.Text = patient.FirstName;
            LastName.Text = patient.LastName;
            BloodType.SelectedIndex = (int)patient.BloodType; 
            Height.Text = patient.Height.ToString();
            Weight.Text = patient.Weight.ToString();
            Informations.Text = patient.Informations;
            ID = patient.ID;
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
            AccountController.EditUrgentPatientAccount(newUrgentPatient, ID);

            UrgentPatientView.Refresh();
            Close();
        }
    }
}
