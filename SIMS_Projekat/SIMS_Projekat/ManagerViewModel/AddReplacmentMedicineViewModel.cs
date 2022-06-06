using System;
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
using SIMS_Projekat.DTO;

namespace SIMS_Projekat.ManagerViewModel
{
    public class AddReplacmentMedicineViewModel : INotifyPropertyChanged
    {
        private Medicine _medicine;
        private int _mood;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Medicine> _allMedicine;
        public ObservableCollection<Medicine> _replacmentMedicine;
        private Medicine _selectedItem;
        private RelayCommand done;
        private RelayCommand addReplacmentMedicine;

        public AddReplacmentMedicineViewModel(Medicine medicine, int mood)
        {
            _medicine = medicine;
            _mood = mood;
            AllMedicine = new ObservableCollection<Medicine>(App.medicineController.GetVerifyMedicine());
            ReplacmentMedicine= new ObservableCollection<Medicine>(_medicine.ReplacmentMedicine);
        }

        public ObservableCollection<Medicine> AllMedicine
        {
            get { return _allMedicine; }
            set
            {
                _allMedicine = value;
                OnPropertyChanged(nameof(AllMedicine));
            }
        }

        public ObservableCollection<Medicine> ReplacmentMedicine
        {
            get { return _replacmentMedicine; }
            set
            {
                _replacmentMedicine = value;
                OnPropertyChanged(nameof(ReplacmentMedicine));
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
            return SelectedItem != null && !ReplacmentMedicine.Contains(SelectedItem);
        }

        public RelayCommand AddReplacmentMedicine
        {
            get
            {
                return addReplacmentMedicine ?? (new RelayCommand(param => DodajZamenskeBtn_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand Done
        {
            get
            {
                return done ?? (new RelayCommand(param => Zavrsi_Click()));
            }
        }

        private void Zavrsi_Click()
        {
            if (_mood == 1)
                ManagerHome.mainFrame.Content = new AddMedicineView(_medicine);
            else
                ManagerHome.mainFrame.Content = new EditMedicineView(_medicine);
        }

        private void DodajZamenskeBtn_Click()
        {
            _medicine.ReplacmentMedicine.Add(SelectedItem);
            ReplacmentMedicine = new ObservableCollection<Medicine>(_medicine.ReplacmentMedicine);
            var dto = new ReplacmentMedicineDTO();
            dto.dtoID = Guid.NewGuid().ToString();
            dto.mainMedicineID = _medicine.MedicineID;
            dto.replacmentMedicineID = SelectedItem.MedicineID;
            App.medicineReplacmentRepository.AddDTO(dto);
        }


        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
