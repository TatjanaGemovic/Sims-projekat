using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class FreeDayRequestController
    {
        public FreeDayRequestService freeDayRequestService;

        public FreeDayRequestController(FreeDayRequestService Service)
        {
            freeDayRequestService = Service;
        }

        public List<FreeDayRequest> GetRequests()
        {
            return freeDayRequestService.GetRequests();
        }

        public List<FreeDayRequest> GetRequestsByDoctor(Doctor d)
        {
            return freeDayRequestService.GetRequestsByDoctor(d);
        }

        public FreeDayRequest AddRequest(FreeDayRequest newRequest)
        {
            return freeDayRequestService.AddRequest(newRequest);
        }

        public bool CanSendRequest(bool urgent, DateTime from, DateTime until, Doctor doctor)
        {
            return freeDayRequestService.CanSendRequest(urgent, from, until, doctor);
        }
    }
}
