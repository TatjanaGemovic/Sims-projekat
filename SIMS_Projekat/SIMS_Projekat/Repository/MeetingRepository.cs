using SIMS_Projekat.Serialization;
using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
    public class MeetingRepository
    {
        public List<Meeting> Meetings { get; set; }
  

        private Serializer<Meeting> meetingSerializer;

        private string meetingsFile;

        private int ID;

        public MeetingRepository(string meetingsFileName)
        {
            Meetings = new List<Meeting>();
            meetingSerializer = new Serializer<Meeting>();
            meetingsFile = meetingsFileName;
            ID = 100;
        }

        public void Serialize()
        {
            meetingSerializer.toCSV(meetingsFile, Meetings);
        }

        public void Deserialize()
        {
            Meetings = meetingSerializer.fromCSV(meetingsFile);

            int maxID = 100;
            foreach(Meeting meeting in Meetings)
            {
                if (int.Parse(meeting.ID) > maxID)
                    maxID = int.Parse(meeting.ID);
            }
            ID = ++maxID;
        }
        public Meeting CreateMeeting(Meeting meeting)
        {
            meeting.ID = ID++.ToString();
            Meetings.Add(meeting);
            return meeting;
        }

        public Meeting DeleteMeeting(Meeting meeting)
        {
            return Meetings.Remove(meeting) ? meeting : null;
        }

        public Meeting EditMeeting(Meeting meeting, string meetingID)
        {
            foreach (Meeting oldMeeting in Meetings)
            {
                if (oldMeeting.ID.Equals(meetingID))
                {
                    oldMeeting.Topic = meeting.Topic;
                    oldMeeting.StartDateTime = meeting.StartDateTime;
                    oldMeeting.EndDateTime = meeting.EndDateTime;
                    oldMeeting.Description = meeting.Description;
                    oldMeeting.InvitedStaff = meeting.InvitedStaff;
                    return oldMeeting;
                }
            }
            return null;
        }

        public List<Meeting> GetAllMeetings()
        {
            return Meetings;
        }

        public Meeting GetMeetingByID(string meetingID)
        {
            foreach (Meeting meeting in Meetings)
            {
                if (meeting.ID.Equals(meetingID))
                    return meeting;
            }
            return null;
        }
    }
}