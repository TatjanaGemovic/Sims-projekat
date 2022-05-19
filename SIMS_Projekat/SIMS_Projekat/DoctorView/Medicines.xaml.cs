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
            InitializeComponent();
            Frame = main;
            doctor = d;
            medicineInformations = new BindingList<MedicineInformation>();
            createList();

            MedicinesLists.ItemsSource = medicineInformations;
            this.DataContext = this;
        }

        public class MedicineInformation
        {
            public string medicineName { get; set; }
            public string Id { get; set; }

            public MedicineInformation(string name, string id)
            {
                medicineName = name;
                Id = id;
            }
        }
        public void createList()
        {
            foreach (Medicine med in App.medicineRepository.GetSendToDoctorMedicine(doctor.ID))
            {
                    medicineInformations.Add(new MedicineInformation(med.MedicineName, med.MedicineID));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MedicineInformation med = (MedicineInformation)MedicinesLists.SelectedItem;

            string id = med.Id;

            Medicine medicine = App.medicineRepository.GetMedicineByID(id);
            Frame.Content = new MedicineInfo(Frame, doctor, medicine);
        }

        private void MedicinesLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MedicinesLists.SelectedItem != null)
            {
                Show.IsEnabled = true;
            }
        }
    }
}
