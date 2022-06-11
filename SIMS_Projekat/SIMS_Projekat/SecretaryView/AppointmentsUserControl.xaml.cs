using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AppointmentsView.xaml
    /// </summary>
    public partial class AppointmentsUserControl : UserControl
    {
        private RoomController roomController;
        private AppointmentController appointmentController;
        private AccountController accountController;
        public ObservableCollection<Room> Rooms { get; set; }
        public static ObservableCollection<Appointment> Appointments { get; set; }

        private static DataGrid dataGrid;

        private ContentControl contentControl;
        public AppointmentsUserControl(RoomController roomController, AccountController accountController, AppointmentController appointmentController, ContentControl contentControl)
        {
            InitializeComponent();
            this.DataContext = this;
            this.roomController = roomController;
            this.accountController = accountController;
            this.appointmentController = appointmentController;
            this.contentControl = contentControl;
            dataGrid = dataGridAppointments;

            Appointments = new ObservableCollection<Appointment>();
            Rooms = new ObservableCollection<Room>(this.roomController.GetAvailableNotMeetingRooms().OrderBy(room => room.RoomNumber));

            SortDataGrid();
           
        }

        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void DatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }


        private void UpdateDataGrid()
        {
            Room selectedRoom = GetSelectedRoom();
            DateTime selectedDate = GetSelectedDate();

            List<Appointment> selectedAppointments = appointmentController.GetAppointmentsByRoomIdAndDate(selectedRoom.RoomID, selectedDate);

            Appointments.Clear();
            foreach (Appointment appointment in selectedAppointments)
            {
                Appointments.Add(appointment);
            }
        }

        private DateTime GetSelectedDate()
        {
            if (DatePicker.SelectedDate == null)
                return DateTime.Today;
            return (DateTime)DatePicker.SelectedDate;
        }

        private Room GetSelectedRoom()
        {
            if (RoomComboBox.SelectedItem == null)
                return Rooms.First();
            return (Room)RoomComboBox.SelectedItem;
        }

        public static void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
            SortDataGrid();
        }

        public static void SortDataGrid()
        {
            dataGrid.Items.SortDescriptions.Clear();
            dataGrid.Items.SortDescriptions.Add(new SortDescription("beginningDate", ListSortDirection.Ascending));
            dataGrid.Items.Refresh();
        }

        private void NewAppointment_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = GetSelectedDate();
            Room selectedRoom = GetSelectedRoom();
            AddAppointmentUserControl addAppointmentUserControl = new AddAppointmentUserControl(roomController, appointmentController, accountController, contentControl ,this, selectedDate, selectedRoom);
            contentControl.Content = addAppointmentUserControl;
        }

        private void EditAppointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointment = (Appointment)dataGridAppointments.SelectedItem;
            EditAppointmentUserControl editAppointmentUserControl = new EditAppointmentUserControl(roomController, appointmentController, accountController, contentControl, this, selectedAppointment);
            contentControl.Content = editAppointmentUserControl;
        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridAppointments.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati termin");
                return;
            }

            Appointment appointment = (Appointment)dataGridAppointments.SelectedItem;
            Appointments.Remove(appointment);
            appointmentController.DeleteAppointment(appointment);
        }
    }
}
