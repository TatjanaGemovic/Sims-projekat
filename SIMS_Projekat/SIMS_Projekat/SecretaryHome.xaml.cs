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
        private AddUrgentPatientUserControl addUrgentPatientUserControl;
        private EquipmentUserControl equipmentUserControl;
        private MeetingsUserControl meetingsUserControl;

        private readonly AccountController accountController;
        private readonly AccountRepository accountRepository;

        private readonly AllergenController allergenController;

        private readonly AppointmentController appointmentController;
        private readonly AppointmentRepository appointmentRepository;

        private readonly RoomController roomController;

        private readonly EquipmentController equipmentController;
        private readonly EquipmentOrderController equipmentOrderController;

        private readonly MeetingController meetingController;

        public SecretaryHome(AccountRepository repository, AccountController controller, 
            AllergenController newAllergenController, RoomController newRoomController)
        {
            InitializeComponent();
            accountController = controller;
            accountRepository = repository;
            allergenController = newAllergenController;
            roomController = newRoomController;

            appointmentController = App.appointmentController;
            appointmentRepository = App.appointmentRepo;

            equipmentController = App.equipmentController;
            equipmentOrderController = App.EquipmentOrderController;

            meetingController = App.MeetingController;


            accountsView = new AccountsView(accountRepository, accountController, allergenController, ContentControl);
            appointmentsUserControl = new AppointmentsUserControl(roomController, accountController, 
                appointmentController, ContentControl);
            allergensUserControl = new AllergensUserControl(allergenController);
            addUrgentPatientUserControl = new AddUrgentPatientUserControl(accountController, roomController, 
                appointmentController, ContentControl, accountsView, Accounts_RadioButton);
            equipmentUserControl = new EquipmentUserControl(equipmentController, equipmentOrderController, ContentControl);

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
            equipmentOrderController.Serialize();
            meetingController.Serialize();
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
            addUrgentPatientUserControl = new AddUrgentPatientUserControl(accountController, roomController,
                appointmentController, ContentControl, accountsView, Accounts_RadioButton);
            ContentControl.Content = addUrgentPatientUserControl;
        }

        private void EquipmentRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            equipmentUserControl = new EquipmentUserControl(equipmentController, equipmentOrderController, ContentControl);
            ContentControl.Content = equipmentUserControl;
        }

        private void MeetingsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            meetingsUserControl = new MeetingsUserControl(meetingController);
            ContentControl.Content = meetingsUserControl;

        }
    }
}
