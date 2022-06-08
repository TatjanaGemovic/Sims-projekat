using SIMS.CompositeComon;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ExchangeEquipmentToMagacin.xaml
    /// </summary>
    public partial class ExchangeEquipmentToMagacin : Page
    {
        private Room _fromRoom;
        private Equipment selectedEquipment;
        private RelayCommand potvrdi;
        private RelayCommand otkazi;

        public RelayCommand Potvrdi
        {
            get
            {
                return potvrdi ?? (new RelayCommand(param => PotvrdiPrebacivanje_Click(), param => canCommandExecut()));
            }
        }

        public RelayCommand OtkaziComannd
        {
            get
            {
                return otkazi ?? (new RelayCommand(param => Otkazi_Click()));
            }
        }

        private Boolean canCommandExecut()
        {
            return !kolicina.Text.Equals("") && datum.SelectedDate != null && int.Parse(kolicina.Text) <= int.Parse(dostupnaKolicina.Text);
                
        }

        public ExchangeEquipmentToMagacin(Model.Equipment oldEquipment, Model.Room fromRoom, int br)
        {
            InitializeComponent();
            this.DataContext = this;
            _fromRoom = fromRoom;
            selectedEquipment = oldEquipment;
            fillForm(br);
        }

        private void PotvrdiPrebacivanje_Click()
        {
            createRequest();
            ManagerHome.mainFrame.Content = new RoomView();

        }

        private void fillForm(int br)
        {
            nazivOpreme.Text = selectedEquipment.EquipmentName;
            dostupnaKolicina.Text = br.ToString();
        }

        private void createRequest()
        {
            Model.ExchangeEquipmentRequest n = new ExchangeEquipmentRequest();
            n.quantity = int.Parse(kolicina.Text);
            n.equipmentID = selectedEquipment.EquipmentID;
            n.scheduleDate = DateTime.Parse(datum.Text);
            n.toRoomID = "magacin";
            n.fromRoomID = _fromRoom.RoomID;
            if (int.Parse(kolicina.Text) == int.Parse(dostupnaKolicina.Text))
            {
                n.allEquipmentFromRoom = true;
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

        private void Otkazi_Click()
        {
            ManagerHome.mainFrame.Content = new RoomView();
        }

    }
}
