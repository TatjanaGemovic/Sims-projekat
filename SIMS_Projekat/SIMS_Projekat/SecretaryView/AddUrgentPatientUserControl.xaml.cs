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
    /// Interaction logic for AddUrgentPatientUserControl .xaml
    /// </summary>
    public partial class AddUrgentPatientUserControl : UserControl
    {
        public AccountController AccountController { get; set; }

        private ContentControl contentControl;
        private UserControl accountsView;
        private RadioButton accountsRadioButton;

        public AddUrgentPatientUserControl(AccountController accountController, ContentControl contentControl, UserControl accountsView, RadioButton radioButton)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            this.accountsView = accountsView;
            AccountController = accountController;
            accountsRadioButton = radioButton;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            var newPatient = new Patient()
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DateOfBirth = DateTime.Parse(Date.Text),
                BloodType = (BloodType)BloodType.SelectedIndex,
                Symptoms = Symptoms.Text,
                IsUrgent = true,
                MedicalRecord = App.medRecordRepository.CreateMedicalRecord(new MedicalRecord()),
                Allergens = new List<Allergen>(),
                month = DateTime.Now.Month,
                year = DateTime.Now.Year,
                numberOfCancelledAppointments = 0

            };
            AccountController.CreatePatientAccount(newPatient);
            AccountsView.AddPatient(newPatient);
            contentControl.Content = accountsView;
            accountsRadioButton.IsChecked = true;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            accountsRadioButton.IsChecked = true;
            contentControl.Content = accountsView;
        }

    }
}
