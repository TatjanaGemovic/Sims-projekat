using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView;
using SIMS_Projekat.Repository;
using SIMS_Projekat.SecretaryView;
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
using System.Windows.Shapes;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for SecretaryHome.xaml
    /// </summary>
    public partial class SecretaryHome : Window
    {
        public AccountRepository AccountRepository { get; set; }
        public AccountController AccountController { get; set; }

        private static DataGrid dataGrid;
        public static ObservableCollection<Account> Patients
        {
            get;
            set;
        }

        public SecretaryHome(AccountRepository accountRepository, AccountController accountController)
        {
            InitializeComponent();
            this.DataContext = this;
            AccountRepository = accountRepository;
            AccountController = accountController;
            dataGrid = dataGridPatients;
            Patients = new ObservableCollection<Account>();
            foreach(Patient patient in AccountRepository.Patients)
            {
                Patients.Add(patient);
            }
            
        }

        public static void AddPatient(Patient newPatient)
        {
            Patients.Add(newPatient);
        }

        public static void DeletePatient(Patient patient)
        {
            Patients.Remove(patient);
        }

        public static void Refresh()
        {
            dataGrid.Items.Refresh();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new AddPatient(AccountController);
            addPatient.Show();
        }

        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGridPatients.SelectedItem;
            DeletePatient(patient);
            AccountController.DeletePatientAccount(patient);
        }


        private void EditPatient(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGridPatients.SelectedItem;
            EditPatient editPatient = new EditPatient(AccountController, patient);
            editPatient.Show();
        }

        private void ShowPatient(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGridPatients.SelectedItem;
            ViewPatient viewPatient = new ViewPatient(patient);
            viewPatient.Show();
        }

        private void DataWindow_Closing(object sender, EventArgs e)
        {
            AccountRepository.Serialize();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddUrgentPatient addUrgentPatient = new(AccountController);
            addUrgentPatient.Show();
        }
    }
}
