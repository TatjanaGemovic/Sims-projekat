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
    /// Interaction logic for SendMedicineToDoctorView.xaml
    /// </summary>
    public partial class SendMedicineToDoctorView : Page,  INotifyPropertyChanged
    {
        private Medicine _medicine;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Doctor> _doctors;
        private int _mood;
        public SendMedicineToDoctorView(Medicine medicine, int mood)
        {
            InitializeComponent();
            this.DataContext = this;
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


        private void Zavrsi_Click(object sender, RoutedEventArgs e)
        {
            var selectedDoctor = (Doctor)datagGridDoctors.SelectedItem;
            _medicine.DoctorRevision = selectedDoctor.ID;
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

        private void OdustaniBtn_Click(object sender, RoutedEventArgs e)
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
