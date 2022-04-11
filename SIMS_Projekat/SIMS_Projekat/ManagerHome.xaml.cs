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
using System.Windows.Shapes;

namespace SIMS_Projekat
{
    /// <summary>
    /// Interaction logic for ManagerHome.xaml
    /// </summary>
    public partial class ManagerHome : Window
    {
        private RoomController roomController;
        public static ObservableCollection<Room> Rooms { get; set; }

        public ManagerHome()
        {
            InitializeComponent();
            this.DataContext = this;
            roomController = new RoomController();
            Rooms = roomController.GetRooms();

        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            roomController.AddRoom(getRoomFromForm());
            clearForm();
            roomController.Serialize();
        }

        private void datagGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Edit(object sender, RoutedEventArgs e)
        {
            var oldRoomID = ((Room)datagGridRooms.SelectedItem).RoomID;
            roomController.EditRoom(oldRoomID, getRoomFromForm());
            datagGridRooms.Items.Refresh();
            clearForm();
            roomController.Serialize();
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = (Room)datagGridRooms.SelectedItem;
            roomController.DeleteRoomByID(selectedRoom.RoomID);
            roomController.Serialize();
        }

        private Room getRoomFromForm()
        {
            Room newRoom = new Room();
            newRoom.RoomID = ID.Text;
            newRoom.RoomNumber = int.Parse(roomNumber.Text);
            newRoom.Floor = int.Parse(roomFloor.Text);
            newRoom.Type = (RoomType)roomType.SelectedIndex;
            return newRoom;
        }

        private void clearForm() {
            ID.Clear();
            roomNumber.Clear();
            roomFloor.Clear();
            roomType.SelectedIndex = -1;
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            if (datagGridRooms.SelectedIndex != -1)
            {
                var selectedRoom = (Room)datagGridRooms.SelectedItem;
                ID.Text = selectedRoom.RoomID;
                roomNumber.Text = selectedRoom.RoomNumber.ToString();
                roomFloor.Text = selectedRoom.Floor.ToString();
                roomType.SelectedIndex = (int)selectedRoom.Type;
            }
        }
    }
}
