using SIMS.CompositeComon;
using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using SIMS_Projekat.Properties;
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
    /// Interaction logic for AddEquipmentView.xaml
    /// </summary>
    public partial class AddEquipmentView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private EquipmentController equipmentController;
        private string _kolicina;
        private string _naziv;
        private int _selectedIndex;

        public AddEquipmentView()
        {
            InitializeComponent();
            this.DataContext = this;
            equipmentController = App.equipmentController;

        }


        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged(nameof(SelectedIndex)); }
        }
        public string KolicinaP
        {
            get { return _kolicina; }
            set { _kolicina = value; OnPropertyChanged(nameof(KolicinaP)); }
        }
        
        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; OnPropertyChanged(nameof(Naziv)); }
        }


        private Equipment getEquipmentFromForm()
        {
            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentID = Guid.NewGuid().ToString();
            newEquipment.EquipmentName = equipmentName.Text;
            newEquipment.Quantity = int.Parse(KolicinaP);
            newEquipment.pEquipmentType = (EquipmentType)equipmentType.SelectedIndex;
            return newEquipment;
        }
        private void QuitAdding_Click(object sender, RoutedEventArgs e)
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

        private void equipmentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AddEquipment.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (string.IsNullOrEmpty(equipmentName.Text) || string.IsNullOrEmpty(equipmentQuantity.Text))
            {
                AddEquipment.IsEnabled = false;
            }
        }

        private void equipmentQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AddEquipment.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (string.IsNullOrEmpty(equipmentName.Text) || string.IsNullOrEmpty(equipmentQuantity.Text))
            {
                AddEquipment.IsEnabled = false;
            }
        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            if(equipmentController.AddEquipment(getEquipmentFromForm())!= null)
                ManagerHome.mainFrame.Content = new EquipmentView();
            else
            {
                if (Settings.Default.CurrentLanguage == "sr-LATN")
                    AutoClosingMessageBox.Show("Oprema sa datim nazivom već postoji u sistemu!", "Greška pri dodavanju opreme", 2000);
                else
                    AutoClosingMessageBox.Show("Equipment with this name exist in base!", "Aad equipment error", 2000);
            }
        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }
    }
}
