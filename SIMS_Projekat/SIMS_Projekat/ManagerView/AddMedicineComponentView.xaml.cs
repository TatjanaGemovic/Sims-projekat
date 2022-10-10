using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using SIMS_Projekat.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for AddMedicineComponentView.xaml
    /// </summary>
    public partial class AddMedicineComponentView : Page, INotifyPropertyChanged
    {
        private Medicine _medicine;
        public event PropertyChangedEventHandler PropertyChanged;
        private int _mood;
        private string _sastojak;

        public string Sastojak
        {
            get { return _sastojak; }
            set { _sastojak = value; OnPropertyChanged(nameof(Sastojak)); }
        }

        public AddMedicineComponentView(Medicine medicine, int mood)
        {
            InitializeComponent();
            this.DataContext = this;
            _medicine = medicine;
            _mood = mood;
            ComponentList.ItemsSource = new ObservableCollection<string>(_medicine.MedicineComponents);
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addToTable();
            var newDTO = createDTO(); 
            SastojakTextBox.Clear();
            App.medicineComponentsRepository.AddDTO(newDTO);
            

        }

        private void addToTable()
        {
            _medicine.MedicineComponents.Add(SastojakTextBox.Text);
            ComponentList.ItemsSource = new ObservableCollection<string>(_medicine.MedicineComponents);
        }

        private MedicineComponentDTO createDTO()
        {
            var dto = new MedicineComponentDTO();
            dto.dtoID = Guid.NewGuid().ToString();
            dto.mainMedicineID = _medicine.MedicineID;
            dto.component = SastojakTextBox.Text;
            return dto;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.CurrentLanguage == "sr-LATN")
            {
                if (MessageBox.Show("Da li ste sigurni da želite da obrišete sastojak?",
                    "Brisanje sastojka",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    refreshTable();
                    var dtoForDelete = App.medicineComponentsRepository.GetDTOByMedicineAndComponent(_medicine.MedicineID, getSelectedItem());
                    App.medicineComponentsRepository.DeleteDTO(dtoForDelete);
                }

            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete component?",
                        "Delete component",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    refreshTable();
                    var dtoForDelete = App.medicineComponentsRepository.GetDTOByMedicineAndComponent(_medicine.MedicineID, getSelectedItem());
                    App.medicineComponentsRepository.DeleteDTO(dtoForDelete);
                }

            }
            
        }

        private void refreshTable()
        {
            _medicine.MedicineComponents.Remove(getSelectedItem());
            ComponentList.ItemsSource = new ObservableCollection<string>(_medicine.MedicineComponents);
        }

        private string getSelectedItem()
        { 
            return (string)ComponentList.SelectedItem;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (_mood == 1)
                ManagerHome.mainFrame.Content = new AddMedicineView(_medicine);
            else
                ManagerHome.mainFrame.Content = new EditMedicineView(_medicine);

        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ComponentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComponentList.SelectedItem != null)
                DeleteComponentBtn.IsEnabled = true;
        }

        private void SastojakTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            DodajBtn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (string.IsNullOrEmpty(SastojakTextBox.Text))
                DodajBtn.IsEnabled = false;
        }

        
    }
}
