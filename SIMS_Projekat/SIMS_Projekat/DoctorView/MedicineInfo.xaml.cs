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

        Frame Frame;
        Doctor doctor;
        Medicine medicine;
        public MedicineInfo(Frame main, Doctor d, Medicine med)
        {
            Frame = main;
            doctor = d;
            medicine = med;
            InitializeComponent();
            Dose.Text = medicine.MedicineDose.ToString();
            Type.Text =medicine.pMedicineType.ToString();
            UseType.Text = medicine.pMedicineUseType.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Medicines(Frame, doctor);
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {

            Medicine med = new Medicine()
            {
                medicineName = medicine.medicineName,
                medicineType = medicine.medicineType,
                medicineUseType = medicine.medicineUseType,
                medicineDose = medicine.medicineDose,
                verify = true,
                sendToDoctor = medicine.sendToDoctor,
                onObservation = false,
                doctorComment = medicine.doctorComment,
            };

            App.medicineRepository.EditMedicine(medicine, med);
            //App.medicineRepository.Serialize();

            Frame.Content = new Medicines(Frame, doctor);
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            Medicine med = new Medicine()
            {
                medicineName = medicine.medicineName,
                medicineType = medicine.medicineType,
                medicineUseType = medicine.medicineUseType,
                medicineDose = medicine.medicineDose,
                verify = false,
                sendToDoctor = medicine.sendToDoctor,
                onObservation = false,
                doctorComment = medicine.doctorComment,
            };

            App.medicineRepository.EditMedicine(medicine, med);
            //App.medicineRepository.Serialize();

            Frame.Content = new Medicines(Frame, doctor);
        }
    }
}
