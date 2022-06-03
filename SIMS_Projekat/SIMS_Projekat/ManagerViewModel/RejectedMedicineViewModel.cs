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
    public class RejectedMedicineViewModel
    {
        public RejectedMedicineViewModel()
        {
            Medicine = new ObservableCollection<Medicine>(App.medicineController.GetRejectMedicine());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Medicine> _medicine;
        private Medicine _selectedItem;
        private RelayCommand editMedicine;
        private RelayCommand deleteMedicine;
        private RelayCommand back;

        public ObservableCollection<Medicine> Medicine
        {
            get { return _medicine; }
            set
            {
                _medicine = value;
                OnPropertyChanged(nameof(Medicine));
            }
        }

        public Medicine SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private Boolean canCommandExecut()
        {
            return SelectedItem != null;
        }
        public RelayCommand Back
        {
            get
            {
                return back ?? (new RelayCommand(param => BackBtn_Click()));
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

        private void ObrisiLekBtn_Click()
        {
            App.medicineController.DeleteMedicine(SelectedItem);
        }



        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void IzmenaLekaBtn_Click()
        {
            ManagerHome.mainFrame.Content = new EditMedicineView(SelectedItem);
        }

        private void BackBtn_Click()
        {
            ManagerHome.mainFrame.Content = new MedicineView();
        }
    }
}
