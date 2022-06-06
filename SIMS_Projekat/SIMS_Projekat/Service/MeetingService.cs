using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class MeetingService
    {
        public MeetingRepository MeetingRepository { get; set; }

        public Meeting AddMeeting(Meeting meeting)
        {
            return MeetingRepository.CreateMeeting(meeting);
        }

        public Meeting DeleteMeeting(Meeting meeting)
        {
            return MeetingRepository.DeleteMeeting(meeting);
        }

        public Meeting EditMeeting(Meeting meeting, string meetingID)
        {
            return MeetingRepository.EditMeeting(meeting, meetingID);
        }

        public List<Meeting> GetAllMeetings()
        {
            return MeetingRepository.Meetings;
        }

        public bool IsMeetingRoomAvailable(Room selectedRoom, DateTime selectedDateTime)
        {
            List<Meeting> meetingsForSelectedRoom = MeetingRepository.Meetings.Where(meeting => meeting.Room.RoomID == selectedRoom.RoomID).ToList();

            if (meetingsForSelectedRoom.Count == 0)
                return true;

            foreach(Meeting meeting in meetingsForSelectedRoom)
            {
                if (meeting.StartDateTime == selectedDateTime)
                    return false;
            }
            return true;
        }

        public void Serialize()
        {
            MeetingRepository.Serialize();
        }

        public void Deserialize()
        {
            MeetingRepository.Deserialize();
        }

    }
}
