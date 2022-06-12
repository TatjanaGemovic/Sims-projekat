﻿using SIMS.CompositeComon;
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
        private RelayCommand potvrdi;
        private string _kolicina;
        private DateTime _datum;
        
        public DateTime SelectedDate
        {
            get { return _datum; }
            set { _datum = value; OnPropertyChanged(nameof(SelectedDate)); }
        }
        public string Kolicina
        {
            get { return _kolicina; }
            set { _kolicina = value; OnPropertyChanged(nameof(Kolicina)); }
        }

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

        public RelayCommand PotvrdiP
        {
            get
            {
                return potvrdi ?? (new RelayCommand(param => PotvrdiPrebacivanje_Click(), param => canCommandExecut()));
            }
        }
        private Boolean canCommandExecut()
        {
            if (_fromRoom != null)
                return !kolicina.Text.Equals("") && int.TryParse(kolicina.Text, out int result) && result <= int.Parse(dostupnaKolicina.Text) && datagGridRooms.SelectedItem!= null && _fromRoom.RoomID != ((Room)datagGridRooms.SelectedItem).RoomID;
            else
                return !kolicina.Text.Equals("")  && int.TryParse(kolicina.Text, out int result)  && result<= int.Parse(dostupnaKolicina.Text) && datagGridRooms.SelectedItem != null;

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
            SelectedDate = DateTime.Now;

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
            {
                n.allEquipmentFromRoom = true;
                if (_fromRoom != null)
                    refreshFromRoomAll();
            }
            else
            {
                n.allEquipmentFromRoom = false;
            }
            n.requestID = n.toRoomID + n.equipmentID;
            App.exchangeEquipmentRequestController.AddRequest(n);
            App.equipmentController.Serialize();

        }
        private void refreshFromRoomAll()
        {
            var ind = _fromRoom.pEquipment.IndexOf(selectedEquipment);
            _fromRoom.pEquipment.Remove(selectedEquipment);
            var brisanje = _fromRoom.pEquipmentQuantity[ind];
            _fromRoom.pEquipmentQuantity.Remove(brisanje);
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void PotvrdiPrebacivanje_Click()
        {
            createRequest();
            if (_fromRoom == null)
                ManagerHome.mainFrame.Content = new EquipmentView();
            else
                ManagerHome.mainFrame.Content = new RoomView();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new EquipmentView();
        }

        private void kolicina_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            PotvrdiPrebacivanje.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (string.IsNullOrEmpty(kolicina.Text))
            {
                PotvrdiPrebacivanje.IsEnabled = false;
            }
        }
    }
}
