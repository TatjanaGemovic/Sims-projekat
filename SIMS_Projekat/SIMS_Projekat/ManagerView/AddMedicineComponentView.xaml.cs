using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
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
            refreshTable();
            var dtoForDelete = App.medicineComponentsRepository.GetDTOByMedicineAndComponent(_medicine.MedicineID, getSelectedItem());
            App.medicineComponentsRepository.DeleteDTO(dtoForDelete);
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
    }
}
