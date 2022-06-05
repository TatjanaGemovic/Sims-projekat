using SIMS_Projekat.Controller;
using SIMS_Projekat.DoctorView.ViewModel;
using SIMS_Projekat.Model;
using SIMS_Projekat.SecretaryView.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_Projekat.SecretaryView.ViewModel
{
    public class AddMeetingViewModel : BindableBase
    {
        private string NOTIFICATION_CONTET = "Imate novi zakazani sastanak";
        private RoomController roomController;
        private MeetingController meetingController;
        private AccountController accountController;
        private INotificationSender notificationSender;

        private ContentControl contentControl;

        public string Topic { get; set; }
        public string Description { get; set; }

        private DateTime _selectedDateTime;
        public DateTime SelectedDateTime
        {
            get { return _selectedDateTime; }
            set 
            { 
                _selectedDateTime = value;
                TimeSpan timeStart = TimeSpan.Parse(_selectedTime);
                _selectedDateTime = _selectedDateTime.Date + timeStart;
                UpdateAvailableMeetingRooms(_selectedDateTime);

            }
        }

        public ObservableCollection<Room> AvailableMeetingRooms { get; set; }

        public Room SelectedRoom { get; set; }

        public ObservableCollection<string> AvailableTimes { get; set; }

        private string _selectedTime = "12:00";
        public string SelectedTime
        {
            get { return _selectedTime; }
            set 
            { 
                _selectedTime = value; 
                
                TimeSpan timeStart = TimeSpan.Parse(_selectedTime);
                SelectedDateTime = SelectedDateTime.Date + timeStart;
                UpdateAvailableMeetingRooms(SelectedDateTime);
                UpdateAvailableDoctors(SelectedDateTime);
            }
        }

        public ObservableCollection<StaffSelection> AvailableStaff { get; set; }

        public MyICommand SaveMeetingCommand { get; set; }
        public MyICommand CancelCommand { get; set; }


        public AddMeetingViewModel(ContentControl contentControl, INotificationSender notificationSender, 
            AppointmentController appointmentController, RoomController roomController, MeetingController meetingController, AccountController accountController)
        {
            this.roomController = roomController;
            this.meetingController = meetingController;
            this.accountController = accountController;
            this.contentControl = contentControl;
            this.notificationSender = notificationSender;

            AvailableTimes = new ObservableCollection<string>(appointmentController.CreateAppointmentTime());
            AvailableMeetingRooms = new ObservableCollection<Room>();
            AvailableStaff = new ObservableCollection<StaffSelection>();

            SaveMeetingCommand = new MyICommand(SaveMeetingExecuteMethod);
            CancelCommand = new MyICommand(CancelExecuteMethod);

            SelectedDateTime = DateTime.Today;
        }

        private void UpdateAvailableMeetingRooms(DateTime selectedDateTime)
        {
            AvailableMeetingRooms.Clear();
            foreach (Room room in roomController.GetAvailableMeetingRooms())
            {
                if(meetingController.IsMeetingRoomAvailable(room, selectedDateTime))
                {
                    AvailableMeetingRooms.Add(room);
                }
            }
        }

        private void UpdateAvailableDoctors(DateTime selectedDateTime)
        {
            AvailableStaff.Clear();
            foreach(Doctor doctor in accountController.GetAvailableDoctors(selectedDateTime))
            {
                AvailableStaff.Add(new StaffSelection(doctor));
            }
        }

        private void SaveMeetingExecuteMethod()
        {
            List<Account> selectedStaff = GetSelectedStaff();
            CreateNewMeeting(selectedStaff);
            SendNotifications(selectedStaff);
           
            contentControl.Content = new MeetingsUserControl(notificationSender, meetingController, contentControl);
        }

        private void CancelExecuteMethod()
        {
            contentControl.Content = new MeetingsUserControl(notificationSender, meetingController, contentControl);
        }

        private List<Account> GetSelectedStaff()
        {
            List<Account> selectedStaff = new List<Account>();
            foreach (StaffSelection staffSelection in AvailableStaff)
            {
                if (staffSelection.IsSelected)
                {
                    selectedStaff.Add(staffSelection.Account);
                }
            }
            return selectedStaff;
        }

        private void CreateNewMeeting(List<Account> selectedStaff)
        {
            Meeting newMeeting = new Meeting()
            {
                Topic = Topic,
                Description = Description,
                StartDateTime = SelectedDateTime,
                EndDateTime = SelectedDateTime.AddMinutes(15),
                Room = SelectedRoom,
                InvitedStaff = selectedStaff
            };

            meetingController.AddMeeting(newMeeting);
        }

        private void SendNotifications(List<Account> selectedStaff)
        {
            foreach(Account account in selectedStaff)
            {
                string notificationContent = NOTIFICATION_CONTET + " " + SelectedDateTime;
                notificationSender.SendNotification(account.ID, notificationContent);
            }
        }

    }


    public class StaffSelection
    {
        public Account Account { get; set; }
        public bool IsSelected { get; set; }

        public StaffSelection(Account account)
        {
            Account = account;
            IsSelected = false;
        }
    }
}
