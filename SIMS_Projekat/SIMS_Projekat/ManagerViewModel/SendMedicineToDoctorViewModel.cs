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
    public class SendMedicineToDoctorViewModel
    {
        private Medicine _medicine;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Doctor> _doctors;
        private int _mood;
        private Doctor selectedItem;
        private RelayCommand done;
        private RelayCommand quit;

        public SendMedicineToDoctorViewModel(Medicine medicine, int mood)
        {
            _medicine = medicine;
            Doctors = new ObservableCollection<Doctor>(App.accountController.GetAllDoctorAccounts());
            _mood = mood;
        }

        public ObservableCollection<Doctor> Doctors
        {
            get { return _doctors; }
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }

        public Doctor SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private Boolean canCommandExecut()
        {
            return SelectedItem != null;
        }

        public RelayCommand Done
        {
            get
            {
                return done ?? (new RelayCommand(param => Zavrsi_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand Quit
        {
            get
            {
                return quit ?? (new RelayCommand(param => OdustaniBtn_Click()));
            }
        }

        private void Zavrsi_Click()
        {
            
            _medicine.DoctorRevision = SelectedItem.ID;
            if (_mood == 1)
            {
                App.medicineController.AddMedicine(_medicine);

            }
            else
            {
                App.medicineController.EditMedicine(App.medicineController.GetMedicineByID(_medicine.MedicineID), _medicine);
            }
            ManagerHome.mainFrame.Content = new MedicineView();

        }

        private void OdustaniBtn_Click()
        {
            ManagerHome.mainFrame.Content = new MedicineView();
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
