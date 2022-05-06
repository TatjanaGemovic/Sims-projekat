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
        public ExchangeEquipmentToMagacin(Model.Equipment oldEquipment, Model.Room fromRoom, int br)
        {
            InitializeComponent();
            this.DataContext = this;
            _fromRoom = fromRoom;
            selectedEquipment = oldEquipment;
            fillForm(br);
        }

        private void PotvrdiPrebacivanje_Click(object sender, RoutedEventArgs e)
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
                n.allEquipmentFromRoom = true;
            else if (int.Parse(kolicina.Text) > int.Parse(dostupnaKolicina.Text))
            { }//greska
            else
            { n.allEquipmentFromRoom = false; }
            n.requestID = n.toRoomID + n.equipmentID;
            App.exchangeEquipmentRequestController.AddRequest(n);
            App.equipmentController.Serialize();

        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome.mainFrame.Content = new RoomView();
        }
    }
}
