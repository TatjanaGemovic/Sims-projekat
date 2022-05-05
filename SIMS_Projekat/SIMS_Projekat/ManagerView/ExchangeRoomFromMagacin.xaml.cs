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
    /// Interaction logic for ExchangeRoomFromMagacin.xaml
    /// </summary>
    public partial class ExchangeRoomFromMagacin : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<Room> rooms { get; set; }
        private Equipment selectedEquipment;
        private Room _fromRoom;

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                if (rooms == value)
                    return;
                rooms = new ObservableCollection<Room>(App.roomController.GetAvailableNotMeetingRooms());
                OnPropertyChanged(nameof(Rooms));
            }
        }
        public ExchangeRoomFromMagacin(Model.Equipment oldEquipment, Model.Room fromRoom, int br)
        {
            InitializeComponent();
            this.DataContext = this;
            _fromRoom = fromRoom;
            if(fromRoom==null)
                Rooms = new ObservableCollection<Room>(App.roomController.GetAvailableNotMeetingRooms());
            else
                Rooms = new ObservableCollection<Room>(App.roomController.GetAvailableNotMeetingRoomsExcept(fromRoom.RoomID));
            selectedEquipment = oldEquipment;
           
            fillForm(br);


        }

        private void fillForm(int br)
        {
            nazivOpreme.Text = selectedEquipment.EquipmentName;

            if (_fromRoom == null)
                dostupnaKolicina.Text = selectedEquipment.Quantity.ToString();
            else
                dostupnaKolicina.Text = br.ToString();
        }



        private void createRequest()
        {
            Model.ExchangeEquipmentRequest n = new ExchangeEquipmentRequest();
            n.quantity = int.Parse(kolicina.Text);
            n.equipmentID = selectedEquipment.EquipmentID;
            n.scheduleDate = DateTime.Parse(datum.Text);
            n.toRoomID = ((Room)datagGridRooms.SelectedItem).RoomID;
            if (_fromRoom == null)
                n.fromRoomID = "magacin";
            else
                n.fromRoomID = _fromRoom.RoomID;
            if (int.Parse(kolicina.Text) == int.Parse(dostupnaKolicina.Text))
                n.allEquipmentFromRoom = true;
            else if (int.Parse(kolicina.Text) > int.Parse(dostupnaKolicina.Text))
            { }//greska
            else
            { n.allEquipmentFromRoom = false; }
            n.requestID = n.toRoomID + n.equipmentID;
            App.exchangeEquipmentRequestController.AddRequest(n);
            App.equipmentController.Serialize();

        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void PotvrdiPrebacivanje_Click(object sender, RoutedEventArgs e)
        {
            createRequest();
            ManagerHome.mainFrame.Content = new EquipmentView();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new EquipmentView();
        }
    }
}
