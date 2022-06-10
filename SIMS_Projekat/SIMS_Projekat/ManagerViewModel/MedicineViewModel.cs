using SIMS.CompositeComon;
using SIMS_Projekat.Controller;
using SIMS_Projekat.ManagerView;
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

namespace SIMS_Projekat.ManagerViewModel
{
    public class MedicineViewModel : INotifyPropertyChanged
    {
        private Medicine _selectedItem;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Medicine> _medicine;
        private RelayCommand addMedicine;
        private RelayCommand editMedicine;
        private RelayCommand deleteMedicine;
        private RelayCommand rejectedMedicine;

        public Medicine SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public MedicineViewModel()
        {
            Medicine = new ObservableCollection<Medicine>(App.medicineController.GetVerifyMedicine());
        }

        public RelayCommand AddMedicine
        {
            get
            {
                return addMedicine ?? (new RelayCommand(param => DodajLekBtn_Click()));
            }
        }

        public RelayCommand EditMedicine
        {
            get
            {
                return editMedicine ?? (new RelayCommand(param => IzmenaLekaBtn_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand DeleteMedicine
        {
            get
            {
                return deleteMedicine ?? (new RelayCommand(param => ObrisiLekBtn_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand RejectedMedicine
        {
            get
            {
                return rejectedMedicine ?? (new RelayCommand(param => OdbijeniLekoviBtn_Click()));
            }
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

        private Boolean canCommandExecut()
        {
            return SelectedItem != null;
        }

        private void ObrisiLekBtn_Click()
        {
            App.medicineController.DeleteMedicine(SelectedItem);
            Medicine = new ObservableCollection<Medicine>(App.medicineController.GetVerifyMedicine());
        }



        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void DodajLekBtn_Click()
        {
            ManagerHome.mainFrame.Content = new AddMedicineView(null);
        }

        private void IzmenaLekaBtn_Click()
        {
            ManagerHome.mainFrame.Content = new EditMedicineView(SelectedItem);
        }

        private void OdbijeniLekoviBtn_Click()
        {
            ManagerHome.mainFrame.Content = new RejectMedicine();
        }

        
    }
}
