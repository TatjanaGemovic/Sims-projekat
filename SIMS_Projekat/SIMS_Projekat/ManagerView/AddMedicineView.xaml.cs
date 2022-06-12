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
    /// Interaction logic for AddMedicineView.xaml
    /// </summary>
    public partial class AddMedicineView : Page, INotifyPropertyChanged
    {
        private Medicine newMedicine;
        public event PropertyChangedEventHandler PropertyChanged;
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

        public AddMedicineView(Medicine medicine)
        {
            InitializeComponent();
            this.DataContext = this;
            if (medicine == null)
            {
                newMedicine = new Medicine();
                medicineType.SelectedIndex = 0;
                medicineUseType.SelectedIndex = 0;
            }
            else
            {
                newMedicine = medicine;
                fillForm();
            }
        }

        private void fillForm()
        {
            medicineName.Text = newMedicine.MedicineName;
            medicineDose.Text = newMedicine.MedicineDose.ToString();
            medicineType.SelectedIndex = (int)newMedicine.pMedicineType;
            medicineUseType.SelectedIndex = (int)newMedicine.pMedicineUseType;
            Naziv = newMedicine.MedicineName;
            Jacina = newMedicine.MedicineDose.ToString();
        }
        private void getMedicineFromForm()
        {
            if (newMedicine.MedicineName == null)
                newMedicine.MedicineID = Guid.NewGuid().ToString();
            newMedicine.MedicineName = medicineName.Text;
            newMedicine.MedicineDose = int.Parse(medicineDose.Text);
            newMedicine.pMedicineType = (MedicineType)medicineType.SelectedIndex;
            newMedicine.pMedicineUseType = (MedicineUseType)medicineUseType.SelectedIndex;
            newMedicine.Verify = false;
            newMedicine.OnObservation = true;
            newMedicine.SendToDoctor = true;
            newMedicine.DoctorComment = "";
            newMedicine.DoctorRevision = "";
        }
        private void QuitAdding_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new MedicineView();
        }


        private void SendToDoctor_Click(object sender, RoutedEventArgs e)
        {
           getMedicineFromForm();
           ManagerHome.mainFrame.Content = new SendMedicineToDoctorView(newMedicine,1);
        }

        private void AddComponent_Btn_Click(object sender, RoutedEventArgs e)
        {
            getMedicineFromForm();
            ManagerHome.mainFrame.Content = new AddMedicineComponentView(newMedicine,1);
        }

        private void AddReplacmentMedicine_Btn_Click(object sender, RoutedEventArgs e)
        {
            getMedicineFromForm();
            ManagerHome.mainFrame.Content = new AddReplacmentMedicineView(newMedicine,1);
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
            if (string.IsNullOrEmpty(medicineName.Text) || string.IsNullOrEmpty(medicineDose.Text)) 
            {
                AddComponent_Btn.IsEnabled = false;
                AddReplacmentMedicine_Btn.IsEnabled = false;
                SendToDoctor_Btn.IsEnabled = false;
            }
               
        }

        private void medicineDose_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AddComponent_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            AddReplacmentMedicine_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            SendToDoctor_Btn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (string.IsNullOrEmpty(medicineName.Text) || string.IsNullOrEmpty(medicineDose.Text))
            {
                AddComponent_Btn.IsEnabled = false;
                AddReplacmentMedicine_Btn.IsEnabled = false;
                SendToDoctor_Btn.IsEnabled = false;
            }
        }
    }
}
