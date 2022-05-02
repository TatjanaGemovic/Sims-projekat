using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddPatientUserControl.xaml
    /// </summary>
    public partial class ViewPatientUserControl : UserControl
    {
        private ContentControl contentControl;
        private UserControl accountsView;

        private string ID;
        public Patient Patient { get; set; }

        public ViewPatientUserControl(ContentControl contentControl, UserControl accountsView, Patient selectedPatient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            this.accountsView = accountsView;

            Patient = selectedPatient;

            FirstName.Text = Patient.FirstName;
            LastName.Text = Patient.LastName;
            Date.Text = Patient.DateOfBirth.ToString();
            Jmbg.Text = Patient.Jmbg;
            Email.Text = Patient.Email;
            BloodType.Text = Patient.BloodType.ToString();
            PhoneNumber.Text = Patient.PhoneNumber;
            HealthInsuranceID.Text = Patient.PhoneNumber;
            Height.Text = Patient.Height.ToString();
            Weight.Text = Patient.Weight.ToString();
            Username.Text = Patient.Username;

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = accountsView;
        }


    }
}
