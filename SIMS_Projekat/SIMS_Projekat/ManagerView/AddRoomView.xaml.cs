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
    /// Interaction logic for AddRoomView.xaml
    /// </summary>
    public partial class AddRoomView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RoomController roomController;
        private string _roomNumber;
        private string _roomFloor;
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged(nameof(SelectedIndex)); }
        }
        public string RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
        } 
        public string RoomFloor
        {
            get { return _roomFloor; }
            set { _roomFloor = value; OnPropertyChanged(nameof(RoomFloor)); }
        }


        public AddRoomView()
        {
            InitializeComponent();
            this.DataContext = this;
            roomController =App.roomController;

        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (roomController.AddRoom(getRoomFromForm()) != null)
                ManagerHome.mainFrame.Content = new RoomView();
            else
            {
                if (Settings.Default.CurrentLanguage == "sr-LATN")
                    AutoClosingMessageBox.Show("Prostorija sa datim brojem već postoji u sistemu!", "Greška pri dodavanju prostorije", 2000);
                else
                    AutoClosingMessageBox.Show("Room with this number exist in base!", "Add room error", 2000);
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
                    MessageBox.Show(text, caption,MessageBoxButton.OK, MessageBoxImage.Error);
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

        private Room getRoomFromForm()
        {
            Room newRoom = new Room();
            newRoom.RoomID = Guid.NewGuid().ToString();
            newRoom.RoomNumber = int.Parse(RoomNumber);
            newRoom.Floor = int.Parse(RoomFloor);
            newRoom.pRoomType = (RoomType)roomType.SelectedIndex;
            newRoom.Available = true;
            return newRoom;
        }
        private void QuitAdding_Click(object sender, RoutedEventArgs e)
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

        private void roomNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AddRoom.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (string.IsNullOrEmpty(roomFloor.Text) || string.IsNullOrEmpty(roomNumber.Text))
                AddRoom.IsEnabled = false;
        }

        private void roomFloor_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AddRoom.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            if (string.IsNullOrEmpty(roomFloor.Text) || string.IsNullOrEmpty(roomNumber.Text))
                AddRoom.IsEnabled = false;
        }
    }
}
