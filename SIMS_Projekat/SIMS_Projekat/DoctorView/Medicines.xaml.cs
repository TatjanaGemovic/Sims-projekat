using SIMS_Projekat.Model;
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
        public Medicines(Frame main, Doctor d)
        {
            Frame = main;
            doctor = d;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new DoctorAppointments(Frame, doctor);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                //AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;
                //int appointmentID = appointmentInformation.appointmentId;
                //Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);
                Frame.Content = new MedicineInfo(Frame, doctor);
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
