﻿using SIMS_Projekat.Model;
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

        public List<Meeting> GetAllAlergens()
        {
            return MeetingService.GetAllMeetings();
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
