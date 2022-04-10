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
    /// Interaction logic for ViewUrgentPatient.xaml
    /// </summary>
    public partial class ViewUrgentPatient : Window
    {
        public AccountController AccountController { get; set; }
        private string ID;
        private UrgentPatient patient;
        public ViewUrgentPatient(AccountController accountController, UrgentPatient oldUrgentPatient)
        {
            InitializeComponent();
            AccountController = accountController;
            patient = oldUrgentPatient;

            FirstName.Text = patient.FirstName;
            LastName.Text = patient.LastName;
            BloodType.Text = patient.BloodType.ToString(); 
            Height.Text = patient.Height.ToString();
            Weight.Text = patient.Weight.ToString();
            Informations.Text = patient.Informations;
            ID = patient.ID;
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
