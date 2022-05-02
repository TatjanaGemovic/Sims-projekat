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
    /// Interaction logic for AllergensUserControl.xaml
    /// </summary>
    public partial class AllergensUserControl : UserControl
    {

        public AllergenController AllergenController { get; set; }
        public static ObservableCollection<Allergen> Allergens { get; set; }
        public AllergensUserControl(AllergenController allergenController)
        {
            InitializeComponent();
            this.DataContext = this;
            AllergenController = allergenController;
            Allergens = new ObservableCollection<Allergen>(AllergenController.GetAllAlergens());
        }

        private void AddAllergen_Click(object sender, RoutedEventArgs e)
        {
            AddAllergen addAllergen = new AddAllergen(AllergenController);
            addAllergen.Show();
        }

        public static void AddAllergen(Allergen allergen)
        {
            Allergens.Add(allergen);
        }

        public static void RemoveAllergen(Allergen allergen)
        {
            Allergens.Remove(allergen);
        }

        private void DeleteAllergen_Click(object sender, RoutedEventArgs e)
        {
            Allergen allergen = (Allergen)dataGridAllergens.SelectedItem;
            RemoveAllergen(allergen);
            AllergenController.DeleteAllergen(allergen);
        }
    }
}
