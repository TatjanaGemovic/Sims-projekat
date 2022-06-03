using SIMS_Projekat.DTO;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for AddReplacmentMedicineView.xaml
    /// </summary>
    public partial class AddReplacmentMedicineView : Page
    {

        private Medicine _medicine;
        private int _mood;
        public AddReplacmentMedicineView(Medicine medicine, int mood)
        {
            InitializeComponent();
            this.DataContext = this;
            _medicine = medicine;
            _mood = mood;
            datagGridMedicine.ItemsSource = new ObservableCollection<Medicine>(App.medicineController.GetVerifyMedicine());
            datagGridReplacmentMedicine.ItemsSource = new ObservableCollection<Medicine>(_medicine.ReplacmentMedicine);
        }

        private void Zavrsi_Click(object sender, RoutedEventArgs e)
        {
            if (_mood == 1)
                ManagerHome.mainFrame.Content = new AddMedicineView(_medicine);
            else
                ManagerHome.mainFrame.Content = new EditMedicineView(_medicine);
        }

        private void DodajZamenskeBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = (Medicine)datagGridMedicine.SelectedItem;
            _medicine.ReplacmentMedicine.Add(item);
            datagGridReplacmentMedicine.ItemsSource =new ObservableCollection<Medicine>(_medicine.ReplacmentMedicine);
            var dto = new ReplacmentMedicineDTO();
            dto.dtoID = Guid.NewGuid().ToString();
            dto.mainMedicineID = _medicine.MedicineID;
            dto.replacmentMedicineID = item.MedicineID;
            App.medicineReplacmentRepository.AddDTO(dto);
        }

        private void datagGridReplacmentMedicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagGridMedicine.SelectedItem != null)
                DodajZamenskeBtn.IsEnabled = true;
        }
    }
}
