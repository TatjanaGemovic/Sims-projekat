using SIMS_Projekat.Controller;
using SIMS_Projekat.DTOModel;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for RenoviranjeView.xaml
    /// </summary>
    public partial class RenoviranjeView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Room _roomForRenovation { get; set; }
        private readonly RenovationRequestController _renovationRequestController;


        public RenoviranjeView(Room roomsForRenovation)
        {
            InitializeComponent();
            this.DataContext = this;
            _roomForRenovation = roomsForRenovation; ;
            _renovationRequestController = App.renovationRequestController;
            gridMergeRoom.ItemsSource = App.roomService.GetRoomsExceptRoom(_roomForRenovation);
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var request = createRequest();
            _renovationRequestController.AddRequest(request);
            //  createDTO(request);
            
            ManagerHome.mainFrame.Content = new RoomView();
        }

       /* private void createDTO(RenovationRequest request)
        {
            foreach (Room room in request.roomsForRenovation)
            {   
                var newDto = new RenovationRoomDTO();
                newDto.dtoID = room.RoomID+request.requestID;
                newDto.roomID = room.RoomID;
                newDto.requestID = request.requestID;
                _renovationRoomDTOController.AddRoomRenovation(newDto);
            }
        }*/

        private RenovationRequest createRequest()
        {
            var renovationRequest = new RenovationRequest();
            renovationRequest.requestID = Guid.NewGuid().ToString();
            renovationRequest.scheduleDateStart = DateTime.Parse(DateStart.Text);
            renovationRequest.scheduleDateEnd = DateTime.Parse(DateEnd.Text);
            renovationRequest.reason = ReasnonBox.Text;
            renovationRequest.check = false;
            renovationRequest.roomsForRenovation = _roomForRenovation.RoomID;
            renovationRequest.renovationType = (RenovationType)comboTip.SelectedIndex;
            if (renovationRequest.renovationType == RenovationType.Merge)
            {
                var selectedRoom = (Room)gridMergeRoom.SelectedItem;
                renovationRequest.roomForMerge = selectedRoom.RoomID;
            }
            else
                renovationRequest.roomForMerge = "";
            return renovationRequest;
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
            ManagerHome.mainFrame.Content = new RoomView();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
