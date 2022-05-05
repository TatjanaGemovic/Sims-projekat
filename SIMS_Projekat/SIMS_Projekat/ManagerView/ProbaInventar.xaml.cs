using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
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
    /// Interaction logic for ProbaInventar.xaml
    /// </summary>
    public partial class ProbaInventar : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<Equipment> equipment;
        public static ObservableCollection<int> equipmentQ;
        private DataGrid roomsGrid { get; set; }
        private Room _selectedRoom;

        public ObservableCollection<Equipment> ppEquipment
        {
            get { return equipment; }
            set
            {
                equipment =value;           
                OnPropertyChanged(nameof(ppEquipment));
            }
        }

        public ObservableCollection<int> ppEquipmentQ
        {
            get { return equipmentQ; }
            set
            {
                equipmentQ = value;
                OnPropertyChanged(nameof(ppEquipmentQ));
            }
        }
        public ProbaInventar(Model.Room selectedRoom)
        {
            InitializeComponent();
            this.DataContext = this;
            _selectedRoom = selectedRoom;
            ppEquipment = new ObservableCollection<Equipment>(_selectedRoom.pEquipment);
            ppEquipmentQ = new ObservableCollection<int>(_selectedRoom.pEquipmentQuantity);
            equipmentDataGrid.ItemsSource = ppEquipment;
 
            //equipmentDataGrid = datagGridRooms;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var _selectedEquipment = (Equipment)equipmentDataGrid.SelectedItem;
            int inx = equipmentDataGrid.SelectedIndex;
            lista.SelectedIndex = equipmentDataGrid.SelectedIndex;
            ManagerHome.mainFrame.Content = new ExchangeRoomFromMagacin(_selectedEquipment, _selectedRoom,(int) lista.SelectedItem) ;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var _selectedEquipment = (Equipment)equipmentDataGrid.SelectedItem;
            int inx = equipmentDataGrid.SelectedIndex;
            lista.SelectedIndex = equipmentDataGrid.SelectedIndex;
            ManagerHome.mainFrame.Content = new ExchangeEquipmentToMagacin(_selectedEquipment, _selectedRoom, (int)lista.SelectedItem);
        }
    }
}
