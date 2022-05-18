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
        public object CurrentView { get; set; }

        private readonly AccountsView accountsView;
        private readonly AppointmentsUserControl appointmentsUserControl;
        private readonly AllergensUserControl allergensUserControl;
        private readonly AddUrgentPatientUserControl addUrgentPatientUserControl;

        private readonly AccountController accountController;
        private readonly AccountRepository accountRepository;

        private readonly AllergenController allergenController;

        private readonly AppointmentController appointmentController;
        private readonly AppointmentRepository appointmentRepository;

        private readonly RoomController roomController;

        public SecretaryHome(AccountRepository repository, AccountController controller, AllergenController newAllergenController, RoomController newRoomController)
        {
            InitializeComponent();
            accountController = controller;
            accountRepository = repository;
            allergenController = newAllergenController;
            roomController = newRoomController;

            appointmentController = App.appointmentController;
            appointmentRepository = App.appointmentRepo;

            accountsView = new AccountsView(accountRepository, accountController, allergenController, ContentControl);
            appointmentsUserControl = new AppointmentsUserControl(roomController, accountController, appointmentController, ContentControl);
            allergensUserControl = new AllergensUserControl(allergenController);
            addUrgentPatientUserControl = new AddUrgentPatientUserControl(accountController, ContentControl, accountsView, Accounts_RadioButton);

            ContentControl.Content = accountsView;
            Accounts_RadioButton.IsChecked = true;
        }

        private void LogOut_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            accountController.Serialize();
            allergenController.Serialize();
            appointmentRepository.Serialize();
        }

        private void Accounts_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = accountsView;
        }
        private void Appointments_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = appointmentsUserControl;
        }

        private void Allergens_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = allergensUserControl;
        }

        private void UrgentPatient_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = addUrgentPatientUserControl;
        }
    }
}
