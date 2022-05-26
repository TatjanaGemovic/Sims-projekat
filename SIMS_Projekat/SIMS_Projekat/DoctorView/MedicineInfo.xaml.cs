using SIMS_Projekat.DoctorView.ViewModel;
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

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for MedicineInfo.xaml
    /// </summary>
    public partial class MedicineInfo : Page
    {
        //Frame Frame;
        //Doctor doctor;
        //Medicine medicine;
        public MedicineInfo(Frame frame, Doctor d, Medicine m)
        {
            //Frame = main;
            //doctor = d;
            //medicine = m;
            InitializeComponent();
            this.DataContext = new MedicineInfoViewModel(frame, d, m);

            //Ime.Text = medicine.MedicineName;
            //Doza.Text = medicine.MedicineDose.ToString();
            //Namena.Text = medicine.pMedicineUseType.ToString();
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Medicines(Frame, doctor);
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            medicine.Verify = true;
            medicine.OnObservation = false;

            App.medicineRepository.Serialize();
            Frame.Content = new Medicines(Frame, doctor);
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            medicine.Verify = false;
            medicine.DoctorComment = Komentar.Text;
            medicine.OnObservation = false;

            App.medicineRepository.Serialize();
            Frame.Content = new Medicines(Frame, doctor);
        }*/
    }
}
