using SIMS.CompositeComon;
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
        private Room _selectedRoom;
        private RelayCommand _magacin;
        private RelayCommand _prostorija;

        public RelayCommand Magacin
        {
            get
            {
                return _magacin ?? (new RelayCommand(param => Button_Click_2(), param => canCommandExecut()));
            }
        }

        public RelayCommand Prostorija
        {
            get
            {
                return _prostorija ?? (new RelayCommand(param => Button_Click_1(), param => canCommandExecut()));
            }
        }

        private Boolean canCommandExecut()
        {
            return gridView.SelectedItem != null;
        }

  

        public ObservableCollection<Equipment> ppEquipment
        {
            get { return equipment; }
            set
            {
                equipment = value;
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
            Podaci = new ObservableCollection<GridItem>();

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

        private void Button_Click_1()
        {
            var selectedIndex = gridView.SelectedIndex;
            var _selectedEquipment = ppEquipment[selectedIndex];
            int  kolicinaZaPrenos = ppEquipmentQ[selectedIndex];
            ManagerHome.mainFrame.Content = new ExchangeRoomFromMagacin(_selectedEquipment, _selectedRoom, kolicinaZaPrenos);
        }

        private void Button_Click_2()
        {
            var selectedIndex = gridView.SelectedIndex;
            var _selectedEquipment = ppEquipment[selectedIndex];
            int kolicinaZaPrenos = ppEquipmentQ[selectedIndex];
            ManagerHome.mainFrame.Content = new ExchangeEquipmentToMagacin(_selectedEquipment, _selectedRoom, kolicinaZaPrenos);
        }


        public class GridItem
        {
            public string naziv { get; set; }
            public EquipmentType tip { get; set; }
            public int kolicina { get; set; }

            public GridItem(string n, EquipmentType tip, int kol)
            {
                this.naziv = n;
                this.tip = tip;
                this.kolicina = kol;
            }


        }

        public static ObservableCollection<GridItem> stvari;

        public ObservableCollection<GridItem> Podaci
        {
            get { return stvari; }
            set
            {
                stvari = value;
                napravi();
                OnPropertyChanged(nameof(Podaci));
            }
        }

        private void napravi()
        {
            for (int i = 0; i != ppEquipment.Count; i++)
            {
                var eq = ppEquipment[i];
                int kol = ppEquipmentQ[i];
                var item = new GridItem(eq.EquipmentName, eq.pEquipmentType,kol);
                stvari.Add(item);
            }
        }

    }

   


}
