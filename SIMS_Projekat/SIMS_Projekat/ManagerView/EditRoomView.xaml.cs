using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for EditRoomView.xaml
    /// </summary>
    public partial class EditRoomView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RoomController roomController {get; set;}
        private Room selectedRoom;
        private string _roomNumber;
        private string _roomFloor;


        public string RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
        }
        public string RoomFloor
        {
            get { return _roomFloor; }
            set { _roomFloor = value; OnPropertyChanged(nameof(RoomFloor)); }
        }
        public EditRoomView(Room oldRoom)
        {
            InitializeComponent();
            this.DataContext = this;
            roomController = App.roomController;
            selectedRoom = oldRoom;
            fillForm();
            

        }

        private void EditRoom_Click(object sender, RoutedEventArgs e)
        {
            roomController.EditRoom(selectedRoom.RoomID,getRoomFromForm());
            ManagerHome.mainFrame.Content = new RoomView();

        }

        private Room getRoomFromForm()
        {
            Room newRoom = new Room();
            newRoom.RoomID = selectedRoom.RoomID;
            newRoom.RoomNumber = int.Parse(selectedRoom.RoomNumber.ToString());
            newRoom.Floor = int.Parse(selectedRoom.Floor.ToString());
            newRoom.pRoomType = (RoomType)roomType.SelectedIndex;
            newRoom.Available = selectedRoom.Available;
            return newRoom;
        }

        private void fillForm()
        {
            RoomNumber= selectedRoom.RoomNumber.ToString();
            RoomFloor = selectedRoom.Floor.ToString();
            roomType.SelectedIndex = (int)selectedRoom.pRoomType;
        }

        private void QuitEditing_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new RoomView();
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void roomNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            EditRoom.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }

        private void roomFloor_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            EditRoom.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }
    }
}
