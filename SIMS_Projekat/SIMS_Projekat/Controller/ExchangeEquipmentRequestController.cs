using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Controller
{
    public class ExchangeEquipmentRequestController
    {
        private readonly ExchangeEquipmentRequestService _exchangeEquipmentRequestService;
        private readonly EquipmentService _equipmentService;

        public ExchangeEquipmentRequestController(EquipmentService equipmentService, ExchangeEquipmentRequestService exchangeEquipmentRequestService ) 
        {
            _exchangeEquipmentRequestService = exchangeEquipmentRequestService;
            _equipmentService = equipmentService;
        }

        public void ThreadFunction()
        {
            _exchangeEquipmentRequestService.ThreadFunction();
        }

        public void AddRequest(ExchangeEquipmentRequest newRequest)
        {
            _exchangeEquipmentRequestService.AddRequest(newRequest);
        }
        public void DeleteRequest(ExchangeEquipmentRequest deleteRequest)
        {

            _exchangeEquipmentRequestService.DeleteRequest(deleteRequest);

        }

        public void Serialize()
        {
            _exchangeEquipmentRequestService.Serialize();
    
        }

        public void Deserialize()
        {
            _exchangeEquipmentRequestService.Deserialize();

        }
    }
}
