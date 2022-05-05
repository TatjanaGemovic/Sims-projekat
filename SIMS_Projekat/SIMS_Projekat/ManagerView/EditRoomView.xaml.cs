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
            newRoom.RoomNumber = int.Parse(roomNumber.Text);
            newRoom.Floor = int.Parse(roomFloor.Text);
            newRoom.pRoomType = (RoomType)roomType.SelectedIndex;
            newRoom.Available = selectedRoom.Available;
            return newRoom;
        }

        private void fillForm()
        {
            roomNumber.Text = selectedRoom.RoomNumber.ToString();
            roomFloor.Text = selectedRoom.Floor.ToString();
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
    }
}
