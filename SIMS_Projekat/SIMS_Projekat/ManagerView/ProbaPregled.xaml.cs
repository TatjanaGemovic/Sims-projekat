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
    /// Interaction logic for ProbaPregled.xaml
    /// </summary>
    public partial class ProbaPregled : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<Room> rooms { get; set; }
        public static ObservableCollection<int> equipmentQ;
        private DataGrid roomsGrid { get; set; }
        private Equipment selectedEquipment;

        public ObservableCollection<Room> rrRooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(rrRooms));
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

        public ProbaPregled(Model.Equipment oldEquipment)
        {
            InitializeComponent();
            this.DataContext = this;
            selectedEquipment = oldEquipment;
            rrRooms = new ObservableCollection<Room>(selectedEquipment.Rooms); 
            ppEquipmentQ = new ObservableCollection<int>(selectedEquipment.ppEquipmentQuantity);
            datagGridRooms.ItemsSource = rrRooms;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ManagerHome.mainFrame.Content = new EquipmentView();

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
