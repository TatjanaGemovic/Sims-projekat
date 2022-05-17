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
    public partial class EditPatientUserControl : UserControl
    {
        public AccountController AccountController { get; set; }
        public AllergenController AllergenController { get; set; }
        public ObservableCollection<AllergenSelection> Allergens { get; set; }

        private ContentControl contentControl;
        private UserControl accountsView;

        private string ID;
        private Patient patient;

        public EditPatientUserControl(AccountController accountController, AllergenController allergenController, ContentControl contentControl, UserControl accountsView, Patient oldPatient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.contentControl = contentControl;
            this.accountsView = accountsView;
            AllergenController = allergenController;
            AccountController = accountController;

            patient = oldPatient;
            Allergens = new ObservableCollection<AllergenSelection>();
            foreach (Allergen allergen in AllergenController.GetAllAlergens())
            {
                Allergens.Add(new AllergenSelection 
                { 
                    Allergen = allergen, 
                    IsSelected = patient.Allergens.Contains(allergen) 
                });
            }

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
                Allergens = selectedAllergens

            };
            AccountController.EditPatientAccount(newPatient, ID);
            AccountsView.Refresh();
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
