using SIMS_Projekat.Controller;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.SecretaryView.ViewModel
{
    public class MeetingsViewModel : BindableBase
    {
        public ObservableCollection<Meeting> Meetings { get; set; }
        public ObservableCollection<Meeting> MeetingsForSelectedDate { get; set; }

        private Meeting _selectedMeeting;
        public Meeting SelectedMeeting
        {
            get { return _selectedMeeting; }
            set 
            {
                if (_selectedMeeting != value)
                {
                    _selectedMeeting = value;
                    OnPropertyChanged("SelectedMeeting");
                }
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set 
            {
                _selectedDate = value;
                MeetingsForSelectedDate.Clear();
                foreach(Meeting meeting in Meetings.Where(meeting => meeting.StartDateTime.Date == _selectedDate.Date)){
                    MeetingsForSelectedDate.Add(meeting);
                }          
            }
        }


        private MeetingController meetingController;


        public MeetingsViewModel(MeetingController meetingController)
        {
            this.meetingController = meetingController;
            Meetings = new ObservableCollection<Meeting>(meetingController.GetAllMeetings());
            MeetingsForSelectedDate = new ObservableCollection<Meeting>();
            _selectedDate = DateTime.Now;
        }

    }
}
