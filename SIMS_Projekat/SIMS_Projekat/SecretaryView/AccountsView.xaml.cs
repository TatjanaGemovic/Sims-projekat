﻿using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AccountsView.xaml
    /// </summary>
    public partial class AccountsView : UserControl
    {
        public AccountRepository AccountRepository { get; set; }
        public AccountController AccountController { get; set; }

        private static DataGrid dataGrid;
        public static ObservableCollection<Account> Patients { get; set; }
        public static ObservableCollection<Account> Doctors { get; set; }

        public AccountsView(AccountRepository accountRepository, AccountController accountController)
        {
            InitializeComponent();
            this.DataContext = this;
            AccountRepository = accountRepository;
            AccountController = accountController;
            dataGrid = dataGridPatients;
            Patients = new ObservableCollection<Account>();
            Doctors = new ObservableCollection<Account>();

            // izmeniti na servis umesto repozitorijuma
            foreach (Patient patient in AccountRepository.Patients)
            {
                Patients.Add(patient);
            }
            foreach (Doctor doctor in AccountRepository.Doctors)
            {
                Doctors.Add(doctor);
            }
        }

        public static void AddPatient(Patient newPatient)
        {
            Patients.Add(newPatient);
        }

        public static void AddDoctor(Doctor newDoctor)
        {
            Doctors.Add(newDoctor);
        }

        public static void DeletePatient(Patient patient)
        {
            Patients.Remove(patient);
        }

        public static void DeleteDoctor(Doctor doctor)
        {
            Doctors.Remove(doctor);
        }

        public static void Refresh()
        {
            // dodati za doktore
            dataGrid.Items.Refresh();
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new AddPatient(AccountController);
            addPatient.Show();
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGridPatients.SelectedItem;
            DeletePatient(patient);
            AccountController.DeletePatientAccount(patient);
        }


        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGridPatients.SelectedItem;
            EditPatient editPatient = new EditPatient(AccountController, patient);
            editPatient.Show();
        }

        private void ShowPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGridPatients.SelectedItem;
            ViewPatient viewPatient = new ViewPatient(patient);
            viewPatient.Show();
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddUrgentPatient addUrgentPatient = new(AccountController);
            addUrgentPatient.Show();
        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            AddDoctor addDoctor = new AddDoctor(AccountController);
            addDoctor.Show();
        }

        private void EditDoctor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowDoctor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteDoctor_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}