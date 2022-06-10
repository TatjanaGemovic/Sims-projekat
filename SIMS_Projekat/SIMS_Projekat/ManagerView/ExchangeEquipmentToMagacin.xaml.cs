using SIMS.CompositeComon;
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
    /// Interaction logic for ExchangeEquipmentToMagacin.xaml
    /// </summary>
    public partial class ExchangeEquipmentToMagacin : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Room _fromRoom;
        private Equipment selectedEquipment;
        private RelayCommand potvrdi;
        private RelayCommand otkazi;
        private string _dostupnaKolicina;
        private string _kolicina;
        private DateTime _datum;
        private string _datumText;

        public string SelectedDateText
        {
            get { return _datumText; }
            set { _datumText = value; OnPropertyChanged(nameof(SelectedDateText)); }
        }
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

        public string DostupnaKolicina
        {
            get { return _dostupnaKolicina; }
            set { _dostupnaKolicina = value; OnPropertyChanged(nameof(DostupnaKolicina)); }
        }


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
            return !kolicina.Text.Equals("") && datum.SelectedDate != null && int.TryParse(kolicina.Text, out int result) && result <= int.Parse(dostupnaKolicina.Text);
                
        }

        public ExchangeEquipmentToMagacin(Model.Equipment oldEquipment, Model.Room fromRoom, int br)
        {
            InitializeComponent();
            this.DataContext = this;
            _fromRoom = fromRoom;
            selectedEquipment = oldEquipment;
            fillForm(br);
            SelectedDate = DateTime.Now;
        }

        private void PotvrdiPrebacivanje_Click()
        {
            createRequest();
            ManagerHome.mainFrame.Content = new RoomView();

        }

        private void fillForm(int br)
        {
            nazivOpreme.Text = selectedEquipment.EquipmentName;
            DostupnaKolicina = br.ToString();
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

        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
