using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class MeetingController
    {
        public MeetingService MeetingService { get; set; }

        public Meeting AddMeeting(Meeting meeting)
        {
            return MeetingService.AddMeeting(meeting);
        }

        public Meeting DeleteMeeting(Meeting meeting)
        {
            return MeetingService.DeleteMeeting(meeting);
        }
        public Meeting EditMeeting(Meeting meeting, string meetingID)
        {
            return MeetingService.EditMeeting(meeting, meetingID);
        }

        public List<Meeting> GetAllMeetings()
        {
            return MeetingService.GetAllMeetings();
        }

        public bool IsMeetingRoomAvailable(Room selectedRoom, DateTime selectedDateTime)
        {
            return MeetingService.IsMeetingRoomAvailable(selectedRoom, selectedDateTime);
        }


        public void Serialize()
        {
            MeetingService.Serialize();
        }

        public void Deserialize()
        {
            MeetingService.Deserialize();
        }
    }
}
