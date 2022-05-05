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

namespace SIMS_Projekat.ManagerView
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static RoomController roomController;
        public ObservableCollection<Room> rooms;

        public ObservableCollection<Room> RoomsR
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(RoomsR));
            }
        }

        public RoomView()
        {
            InitializeComponent();
            DataContext = this;
            RoomsR = new ObservableCollection<Room>(App.roomController.GetAvailableRooms());
            
        }


        private void DodajProstorijuBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new AddRoomView();
        }

        private void ObrisiProstorijuBtn_Click(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = (Room)datagGridRooms.SelectedItem;
            App.roomController.DeleteRoomByID(selectedRoom.RoomID);
        }

        private void IzmenaProstorijeBtn_Click(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = (Room)datagGridRooms.SelectedItem;
            ManagerHome.mainFrame.Content = new EditRoomView(selectedRoom);
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void InventarProstorijeBtn1_Click(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = (Room)datagGridRooms.SelectedItem;
            ManagerHome.mainFrame.Content = new ProbaInventar(selectedRoom);
        }

        private void RenoviranjeProstorijeBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
