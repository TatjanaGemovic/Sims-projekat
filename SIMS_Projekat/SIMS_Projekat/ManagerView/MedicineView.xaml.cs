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
    /// Interaction logic for MedicineView.xaml
    /// </summary>
    public partial class MedicineView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Medicine> _medicine;

        public MedicineView()
        {
            InitializeComponent();
            this.DataContext = this;
            Medicine = new ObservableCollection<Medicine>(App.medicineController.GetVerifyMedicine());
        }

        

        public ObservableCollection<Medicine> Medicine
        {
            get { return _medicine; }
            set
            {
                _medicine = value;
                OnPropertyChanged(nameof(Medicine));
            }
        }


        private void ObrisiLekBtn_Click(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)datagGridMedicine.SelectedItem;
            App.medicineController.DeleteMedicine(selectedMedicine);
        }



        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void DodajLekBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new AddMedicineView(null) ;
        }

        private void IzmenaLekaBtn_Click(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = (Medicine)datagGridMedicine.SelectedItem;
            ManagerHome.mainFrame.Content = new EditMedicineView(selectedMedicine);
        }

    }
}
