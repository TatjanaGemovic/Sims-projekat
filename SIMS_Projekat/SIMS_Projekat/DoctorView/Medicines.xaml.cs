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
            if (MedicinesLists.SelectedItem != null)
            {
                //AppointmentInformation appointmentInformation = (AppointmentInformation)OperationsList.SelectedItem;
                //int appointmentID = appointmentInformation.appointmentId;
                //Appointment appointment = App.appointmentController.GetAppointmentByID(appointmentID);
                Frame.Content = new MedicineInfo(Frame, doctor);
            }
            else
            {
                MessageBox.Show("Niste izabrali lek za prikaz!", "Greska");
            }
        }
    }
}
