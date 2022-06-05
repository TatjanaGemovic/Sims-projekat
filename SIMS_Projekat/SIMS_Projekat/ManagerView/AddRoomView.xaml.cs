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
    /// Interaction logic for AddRoomView.xaml
    /// </summary>
    public partial class AddRoomView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RoomController roomController;


        public AddRoomView()
        {
            InitializeComponent();
            this.DataContext = this;
            roomController =App.roomController;

        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            roomController.AddRoom(getRoomFromForm());
            ManagerHome.mainFrame.Content = new RoomView();
        }

        private Room getRoomFromForm()
        {
            Room newRoom = new Room();
            newRoom.RoomID = Guid.NewGuid().ToString();
            newRoom.RoomNumber = int.Parse(roomNumber.Text);
            newRoom.Floor = int.Parse(roomFloor.Text);
            newRoom.pRoomType = (RoomType)roomType.SelectedIndex;
            newRoom.Available = true;
            return newRoom;
        }
        private void QuitAdding_Click(object sender, RoutedEventArgs e)
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
