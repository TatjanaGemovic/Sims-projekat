using SIMS_Projekat.DTOModel;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class RenovationRequestController
    {
        private readonly RenovationRequestService _renovationRequestService;

        public RenovationRequestController(RenovationRequestService renovationRequestService)
        {
            _renovationRequestService = renovationRequestService;
        }

        public void RunRenovation()
        {
            _renovationRequestService.RunRenovation();
        }

        public void AddRequest(RenovationRequest newRequest)
        {
            _renovationRequestService.AddRequest(newRequest);
        }
        public void DeleteRequest(RenovationRequest deleteRequest)
        {

            _renovationRequestService.DeleteRequest(deleteRequest);

        }

        public List<RenovationRequest> GetAllRequests()
        {
            return _renovationRequestService.GetAllRequest();
        }

        public void Serialize()
        {
            _renovationRequestService.Serialize();

        }

        public void Deserialize()
        {
            _renovationRequestService.Deserialize();

        }
    }
}
