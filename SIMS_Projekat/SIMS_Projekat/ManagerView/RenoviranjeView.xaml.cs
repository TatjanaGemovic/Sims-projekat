﻿using SIMS.CompositeComon;
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
        public RelayCommand zavrsi;
        private Room selectedItem;
        private int selectedIndex;
        private string _razlog;
        private DateTime _datumStart;
        private DateTime _datumEnd;

        public DateTime SelectedDateEnd
        {
            get { return _datumEnd; }
            set { _datumEnd = value; OnPropertyChanged(nameof(SelectedDateEnd)); }
        }
        public DateTime SelectedDateStart
        {
            get { return _datumStart; }
            set { _datumStart = value; OnPropertyChanged(nameof(SelectedDateStart)); }
        }

        public RenoviranjeView(Room roomsForRenovation)
        {
            InitializeComponent();
            this.DataContext = this;
            _roomForRenovation = roomsForRenovation; ;
            _renovationRequestController = App.renovationRequestController;
            gridMergeRoom.ItemsSource = App.roomService.GetRoomsExceptRoom(_roomForRenovation);
            SelectedIndex = 0;
            SelectedDateStart = DateTime.Now;
            SelectedDateEnd = DateTime.Now;

        }

        public Room SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public string Razlog
        {
            get { return _razlog; }
            set
            {
                _razlog = value;
                OnPropertyChanged(nameof(Razlog));
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public RelayCommand ZavrsiC
        {
            get
            {
                return zavrsi ?? (new RelayCommand(param => Button_Click(), param => canCommandExecut()));
            }

        }
        
       

        private Boolean canCommandExecut()
        {
            if (SelectedItem == null && (RenovationType)comboTip.SelectedIndex == RenovationType.Merge)
                return false;
            
            return true && isFormFilled();
        }

        private Boolean isFormFilled()
        {
            if (ReasnonBox.Text != "" && comboTip.SelectedIndex != -1 && DateStart.Text != "" && DateEnd.Text != "")
                return true;

            return false;
        }

        private void Button_Click()
        {
            var request = createRequest();
            _renovationRequestController.AddRequest(request);
            //  createDTO(request);
            
            ManagerHome.mainFrame.Content = new RoomView();
        }

 
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

        private void comboTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((RenovationType)comboTip.SelectedIndex != RenovationType.Merge)
                gridMergeRoom.IsEnabled = false;
            else
            {
                gridMergeRoom.IsEnabled = false;
                gridMergeRoom.UnselectAll();
            }
        }


        private void comboDropDown(object sender, EventArgs e)
        {
            if ((RenovationType)comboTip.SelectedIndex == RenovationType.Merge)
                gridMergeRoom.IsEnabled = true;
            else
            {
                gridMergeRoom.IsEnabled = false;
                gridMergeRoom.UnselectAll();
            }
        }

        private void PROBA(object sender, KeyEventArgs e)
        {
            if ((RenovationType)comboTip.SelectedIndex == RenovationType.Merge)
            {
                gridMergeRoom.IsEnabled = true;

            }
            else
            {
                gridMergeRoom.IsEnabled = false;
                gridMergeRoom.UnselectAll();

            }

            

        }
    }
}
