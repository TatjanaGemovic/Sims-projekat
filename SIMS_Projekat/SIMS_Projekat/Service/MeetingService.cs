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

        public List<Meeting> GetAllMeetings()
        {
            return MeetingRepository.Meetings;
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
