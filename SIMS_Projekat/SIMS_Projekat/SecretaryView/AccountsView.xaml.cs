using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.PatientView;
using SIMS_Projekat.Repository;
using SIMS_Projekat.SecretaryView.Help;
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
        public static AccountController AccountController { get; set; }
        public AllergenController AllergenController { get; set; }

        private static DataGrid dataGrid;
        public static ObservableCollection<Account> Patients { get; set; }
        public static ObservableCollection<Account> Doctors { get; set; }

        private ContentControl contentControl;

        public AccountsView(AccountRepository accountRepository, AccountController accountController, AllergenController allergenController, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataContext = this;
            AccountRepository = accountRepository;
            AccountController = accountController;
            AllergenController = allergenController;
            this.contentControl = contentControl;
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
            AddPatientUserControl addPatientUserControl = new AddPatientUserControl(AccountController, AllergenController, contentControl, this);
            contentControl.Content = addPatientUserControl;
        }

        private void DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)dataGridPatients.SelectedItem;
            if (selectedPatient == null)
            {
                MessageBox.Show("Morate izabrati pacijenta iz tabele");
                return;
            }
            DeletePatient(selectedPatient);
            AccountController.DeletePatientAccount(selectedPatient);
        }


        private void EditPatient_Click(object sender, RoutedEventArgs e)
        {

            Patient selectedPatient = (Patient)dataGridPatients.SelectedItem;
            if(selectedPatient == null)
            {
                MessageBox.Show("Morate izabrati pacijenta iz tabele");
                return;
            }
            EditPatientUserControl editPatientUserControl = new EditPatientUserControl(AccountController, AllergenController, contentControl, this, selectedPatient);
            contentControl.Content = editPatientUserControl;
        }

        private void ShowPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)dataGridPatients.SelectedItem;

            ViewPatientUserControl viewPatientUserControl = new ViewPatientUserControl(contentControl, this, selectedPatient);
            contentControl.Content = viewPatientUserControl;
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


        // Deselect data grid selection
        private void dataGridPatients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dataGrid.SelectedItem = null;
        }


        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }
        }

    }
}
