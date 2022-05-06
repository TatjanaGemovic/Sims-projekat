using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Windows;

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddAllergen : Window
    {
        public AllergenController AllergenController{ get; set; }
        public AddAllergen(AllergenController allergenController)
        {
            InitializeComponent();
            AllergenController = allergenController;
        }

        private void SaveLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var newAllergen = new Allergen()
            {
                Name = AllergenName.Text
            };
            AllergenController.AddAllergen(newAllergen);
            AllergensUserControl.AddAllergen(newAllergen);
            Close();
        }

        private void CloseLabel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
