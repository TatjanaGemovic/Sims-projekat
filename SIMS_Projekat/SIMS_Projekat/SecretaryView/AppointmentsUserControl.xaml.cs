using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIMS_Projekat.SecretaryView
{
    /// <summary>
    /// Interaction logic for AppointmentsView.xaml
    /// </summary>
    public partial class AppointmentsUserControl : UserControl
    {
        private RoomController roomController;
        public ObservableCollection<Room> Rooms { get; set; }
        public AppointmentsUserControl(RoomController roomController)
        {
            InitializeComponent();
            this.DataContext = this;
            this.roomController = roomController;
            Rooms = new ObservableCollection<Room>(roomController.GetRooms().OrderBy(room => room.RoomNumber));
        }
    }
}
