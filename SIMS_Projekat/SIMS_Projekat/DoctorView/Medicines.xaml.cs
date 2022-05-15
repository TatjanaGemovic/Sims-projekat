using SIMS_Projekat.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Projekat.DoctorView
{
    /// <summary>
    /// Interaction logic for Medicines.xaml
    /// </summary>
    public partial class Medicines : Page
    {
        Frame Frame;
        Doctor doctor;
        public BindingList<MedicineInformation> medicineInformations { get; set; }
        public Medicines(Frame main, Doctor d)
        {
            Frame = main;
            doctor = d;
            InitializeComponent();
            medicineInformations = new BindingList<MedicineInformation>();
            createList();

            MedicinesLists.ItemsSource = medicineInformations;
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MedicineInformation medInfo = (MedicineInformation)MedicinesLists.SelectedItem;
            string medId = medInfo.medicineId;
            Medicine med = App.medicineRepository.GetMedicineByID(medId);
            Frame.Content = new MedicineInfo(Frame, doctor, med);
        }

        private void MedicinesLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MedicinesLists.SelectedItem != null)
            {
                Show.IsEnabled = true;
            }
        }

        public class MedicineInformation
        {
            public string medicineId { get; set; }
            public string medicineName { get; set; }

            public MedicineInformation(string medid, string name)
            {
                medicineId = medid;
                medicineName = name;
            }
        }
        public void createList()
        {
            foreach (Medicine med in App.medicineRepository.GetSendToDoctorMedicine())
            {
                medicineInformations.Add(new MedicineInformation(med.MedicineID, med.MedicineName));
            }
        }
    }
}
