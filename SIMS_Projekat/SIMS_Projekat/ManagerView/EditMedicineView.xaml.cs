using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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
    public partial class EditMedicineView : Page
    {
        private Medicine _selectedMedicine;
        public EditMedicineView(Medicine selectedMedicine)
        {
            InitializeComponent();
            this.DataContext = this;
            _selectedMedicine = selectedMedicine;
            fillForm();
        }

        private void fillForm()
        {
            medicineName.Text = _selectedMedicine.MedicineName;
            medicineDose.Text = _selectedMedicine.MedicineDose.ToString();
            medicineType.SelectedIndex = (int)_selectedMedicine.pMedicineType;
            medicineUseType.SelectedIndex = (int)_selectedMedicine.pMedicineUseType;
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
    }
}
