using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
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
    /// Interaction logic for AppointmentsView.xaml
    /// </summary>
    public partial class AppointmentsUserControl : UserControl
    {
        private RoomController roomController;
        private AppointmentController appointmentController;
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public AppointmentsUserControl(RoomController newRoomController)
        {
            InitializeComponent();
            this.DataContext = this;
            roomController = newRoomController;
            appointmentController = App.appointmentController;

            Rooms = new ObservableCollection<Room>(roomController.GetRooms().OrderBy(room => room.RoomNumber));
            Appointments = new ObservableCollection<Appointment>();
           
        }

        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void Calenadar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }


        private void UpdateDataGrid()
        {
            Room selectedRoom = (Room)RoomComboBox.SelectedItem;

            if(Calenadar.SelectedDate == null)
                Calenadar.SelectedDate = DateTime.Today;
            DateTime selectedDate = (DateTime)Calenadar.SelectedDate;

            List<Appointment> selectedAppointments = appointmentController.GetAppointmentsByRoomIdAndDate(selectedRoom.RoomID, selectedDate);

            Appointments.Clear();
            foreach (Appointment appointment in selectedAppointments)
            {
                Appointments.Add(appointment);
            }
        }

        private void NewAppointment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditAppointment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
