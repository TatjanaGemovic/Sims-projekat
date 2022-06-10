using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditMedicineView.xaml
    /// </summary>
    public partial class EditMedicineView : Page,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Medicine _selectedMedicine;
        private string _naziv;
        private string _jacina;


        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; OnPropertyChanged(nameof(Naziv)); }
        }
        public string Jacina
        {
            get { return _jacina; }
            set { _jacina = value; OnPropertyChanged(nameof(Jacina)); }
        }
        public EditMedicineView(Medicine selectedMedicine)
        {
            InitializeComponent();
            this.DataContext = this;
            _selectedMedicine = selectedMedicine;
            fillForm();
        }

        private void fillForm()
        {
           Naziv = _selectedMedicine.MedicineName;
           Jacina = _selectedMedicine.MedicineDose.ToString();
            medicineType.SelectedIndex = (int)_selectedMedicine.pMedicineType;
            medicineUseType.SelectedIndex = (int)_selectedMedicine.pMedicineUseType;
            Naziv = _selectedMedicine.MedicineName;
            Jacina = _selectedMedicine.MedicineDose.ToString();
        }

        private void changeMedicineFromForm()
        {
            _selectedMedicine.MedicineName = medicineName.Text;
            _selectedMedicine.MedicineDose = int.Parse(medicineDose.Text);
            _selectedMedicine.pMedicineType = (MedicineType)medicineType.SelectedIndex;
            _selectedMedicine.pMedicineUseType = (MedicineUseType)medicineUseType.SelectedIndex;
            _selectedMedicine.Verify = false;
            _selectedMedicine.OnObservation = true;
            _selectedMedicine.SendToDoctor = true;
            _selectedMedicine.DoctorComment = "";
            _selectedMedicine.DoctorRevision = "";
        }
        private void QuitAdding_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new MedicineView();
        }


        private void SendToDoctor_Click(object sender, RoutedEventArgs e)
        {
            changeMedicineFromForm();
            ManagerHome.mainFrame.Content = new SendMedicineToDoctorView(_selectedMedicine,2);
        }

        private void AddComponent_Btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new AddMedicineComponentView(_selectedMedicine,2);
        }

        private void AddReplacmentMedicine_Btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new AddReplacmentMedicineView(_selectedMedicine,2);
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void medicineName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AddComponent_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            AddReplacmentMedicine_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            SendToDoctor_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;

        }

        private void medicineDose_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox tb = sender as TextBox;
            AddComponent_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            AddReplacmentMedicine_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            SendToDoctor_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }
    }
}
