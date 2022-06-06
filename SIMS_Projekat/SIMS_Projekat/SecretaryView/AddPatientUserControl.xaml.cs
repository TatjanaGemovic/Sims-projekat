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
    public partial class AddPatientUserControl : UserControl
    {
        public AccountController AccountController { get; set; }
        public AllergenController AllergenController { get; set; }
        public ObservableCollection<AllergenSelection> Allergens { get; set; }

        private ContentControl contentControl;
        private UserControl accountsView;

        public AddPatientUserControl(AccountController accountController, AllergenController allergenController, ContentControl contentControl, UserControl accountsView)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            this.accountsView = accountsView;
            AllergenController = allergenController;
            AccountController = accountController;
            Allergens = new ObservableCollection<AllergenSelection>();
            foreach (Allergen allergen in AllergenController.GetAllAlergens())
            {
                Allergens.Add(new AllergenSelection { Allergen = allergen });
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            List<Allergen> selectedAllergens = new List<Allergen>();
            foreach (AllergenSelection allergenSelection in Allergens)
            {
                if (allergenSelection.IsSelected)
                {
                    selectedAllergens.Add(allergenSelection.Allergen);
                }
            }
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
                MedicalRecord = App.medRecordRepository.CreateMedicalRecord(new MedicalRecord()),
                Allergens = selectedAllergens,
                currentMonthUsableForCancellingAppointmentsByPatient = DateTime.Now.Month,
                currentYearUsableForCancellingAppointmentsByPatient = DateTime.Now.Year,
                numberOfCancelledAppointmentsByPatientMonthly = 0

            };
            AccountController.CreatePatientAccount(newPatient);
            AccountsView.AddPatient(newPatient);
            contentControl.Content = accountsView;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = accountsView;
        }

        public class AllergenSelection
        {
            public Allergen Allergen { get; set; }
            public bool IsSelected { get; set; }

            public AllergenSelection()
            {
                IsSelected = false;
            }
        }

    }
}
