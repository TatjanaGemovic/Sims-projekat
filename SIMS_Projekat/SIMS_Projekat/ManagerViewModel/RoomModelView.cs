using SIMS.CompositeComon;
using SIMS_Projekat.Controller;
using SIMS_Projekat.ManagerView;
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

namespace SIMS_Projekat.ManagerModelView
{
    public class RoomModelView : INotifyPropertyChanged
    {
        private Room _selectedItem;
        private int _selectedIndex;
        public event PropertyChangedEventHandler PropertyChanged;
        public static RoomController roomController;
        public ObservableCollection<Room> rooms;
        private RelayCommand inventarProstorije;
        private RelayCommand addRoom;
        private RelayCommand editRoom;
        private RelayCommand deleteRoom;
        private RelayCommand renovationRoom;

        public Room SelectedItem
        {
            get { return _selectedItem; }
            set 
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public ObservableCollection<Room> RoomsR
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(RoomsR));
            }
        }

        public RoomModelView()
        {

            RoomsR = new ObservableCollection<Room>(App.roomController.GetAvailableRooms());

        }

        public RelayCommand InventarProstorije
        {
            get
            {
                return inventarProstorije ?? (new RelayCommand(param => InventarProstorijeBtn1_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand AddRoom
        {
            get
            {
                return addRoom ?? (new RelayCommand(param => DodajProstorijuBtn_Click()));
            }
        }

        public RelayCommand EditRoom
        {
            get
            {
                return editRoom ?? (new RelayCommand(param => IzmenaProstorijeBtn_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand DeleteRoom
        {
            get
            {
                return deleteRoom ?? (new RelayCommand(param => ObrisiProstorijuBtn_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand RenovationRoom
        {
            get
            {
                return renovationRoom?? (new RelayCommand(param => RenoviranjeProstorijeBtn_Click(), param => canCommandExecut()));
            }
        }

        private Boolean canCommandExecut()
        {
            return SelectedItem != null;
        }

        private void DodajProstorijuBtn_Click()
        {
            ManagerHome.mainFrame.Content = new AddRoomView();
        }

        private void ObrisiProstorijuBtn_Click()
        {
           
            App.roomController.DeleteRoomByID(SelectedItem.RoomID);
            RoomsR = new ObservableCollection<Room>(App.roomController.GetAvailableRooms());
        }

        private void IzmenaProstorijeBtn_Click()
        {
            
            ManagerHome.mainFrame.Content = new EditRoomView(SelectedItem);
        }

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void InventarProstorijeBtn1_Click()
        {
            
            ManagerHome.mainFrame.Content = new ProbaInventar(SelectedItem);
        }

        private void RenoviranjeProstorijeBtn_Click()
        {
            ManagerHome.mainFrame.Content = new RenoviranjeView(SelectedItem);
        }


        

       


    }
}
