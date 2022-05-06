using SIMS_Projekat.DTO;
using SIMS_Projekat.ManagerView;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class ExchangeEquipmentRequestService
    {
        
        private readonly ExchangeEquipmentRequestRepository _requestRepository;
        private readonly EquipmentRepository _equipmentRepository;
        private readonly RoomEquipmentDTORepository _roomEquipmentDTORepository;

        public ExchangeEquipmentRequestService(ExchangeEquipmentRequestRepository exchangeEquipmentRequestRepository, RoomEquipmentDTORepository roomEquipmentDTORepository, EquipmentRepository equipmentRepository)
        {
            _requestRepository = exchangeEquipmentRequestRepository;
            _equipmentRepository = equipmentRepository;
            _roomEquipmentDTORepository = roomEquipmentDTORepository;
            
        }

        public List<ExchangeEquipmentRequest> GetAllRequest()
        {
            return _requestRepository.GetAllRequest();
        }

        public ExchangeEquipmentRequest GetRequestByID(string id)
        {
            return _requestRepository.GetRequestByID(id);
        }
        public void Notify(ExchangeEquipmentRequest request)
        {
            IncreseQuantityInToRoom(request);
            DecreseQuantityInFromRoom(request);

        }

        private void IncreseQuantityInToRoom(ExchangeEquipmentRequest request)
        {
            if (request.toRoomID.Equals("magacin"))
            {
                updateEquipmentRepository(request, 1);
            }
            else
            {
                var dtoToChange = _roomEquipmentDTORepository.GetRoomEquipmentByID(request.toRoomID+request.equipmentID);
                if (dtoToChange!= null)
                     updateDTO(request,1);
                else 
                {
                    var newDTO = createDTO(request);
                    _roomEquipmentDTORepository.AddRoomEquipment(newDTO);
                       
                }
                

            }
        }


        private void DecreseQuantityInFromRoom(ExchangeEquipmentRequest request)
        {
            if (request.fromRoomID.Equals("magacin"))
            {
                updateEquipmentRepository(request, 2);

            }
            else
            {
                if (request.allEquipmentFromRoom == true)
                    deleteDTOData(request);
                else
                {
                    var dtoToChange = _roomEquipmentDTORepository.GetRoomEquipmentByID(request.fromRoomID + request.equipmentID);
                    if (dtoToChange != null)
                    {
                        updateDTO(request, 2);
                    }
                    
                }
                  

            }
        }

        private RoomEquipmentDTO createDTO(ExchangeEquipmentRequest request)
        {
            RoomEquipmentDTO newDTO = new RoomEquipmentDTO();
            newDTO.RoomEquipmentDTOID = request.toRoomID + request.equipmentID;
            newDTO.RoomIDDTO = request.toRoomID;
            newDTO.EquipmentIDDTO = request.equipmentID;
            newDTO.QuantityDTO = request.quantity;
            return newDTO;
        }

        private void updateEquipmentRepository(ExchangeEquipmentRequest request, int mood) 
        {
            var oldEquipment = _equipmentRepository.GetEquipmentByID(request.equipmentID);
            var newEquipment = _equipmentRepository.GetEquipmentByID(request.equipmentID);
            if (oldEquipment != null)
            {
                if(mood==1)
                    newEquipment.Quantity += request.quantity;
                else
                    newEquipment.Quantity -= request.quantity;
                
            }
            _equipmentRepository.EditEquipment(oldEquipment, newEquipment);
        }

        private void updateDTO( ExchangeEquipmentRequest request, int mood)
        {
            var dtoForChange = new RoomEquipmentDTO();
            var newDTO = new RoomEquipmentDTO();
            if (mood == 1)
            {
                dtoForChange= _roomEquipmentDTORepository.GetRoomEquipmentByID(request.toRoomID + request.equipmentID);
                newDTO= _roomEquipmentDTORepository.GetRoomEquipmentByID(request.toRoomID + request.equipmentID);
                newDTO.QuantityDTO += request.quantity;
            }
            else 
            {
                dtoForChange = _roomEquipmentDTORepository.GetRoomEquipmentByID(request.fromRoomID + request.equipmentID);
                newDTO = _roomEquipmentDTORepository.GetRoomEquipmentByID(request.fromRoomID + request.equipmentID);
                newDTO.QuantityDTO -= request.quantity;
            }
            _roomEquipmentDTORepository.EditRoomEquipment(dtoForChange, newDTO);
        }

        private void deleteDTOData(ExchangeEquipmentRequest request)
        {
            var dtoFromRepository = _roomEquipmentDTORepository.GetRoomEquipmentByID(request.fromRoomID+ request.equipmentID);
            if(dtoFromRepository!= null)
                _roomEquipmentDTORepository.DeleteRoomEquipment(dtoFromRepository);

        }
        public ExchangeEquipmentRequest AddRequest(ExchangeEquipmentRequest newRequest)
        {
           //
            return _requestRepository.AddRequest(newRequest);
        }
        public ExchangeEquipmentRequest DeleteRequest(ExchangeEquipmentRequest deleteRequest)
        {
            if (GetRequestByID(deleteRequest.requestID) != null)
            {
                return _requestRepository.DeleteRequest(deleteRequest);
            }

            return null;

        }


        public void Serialize()
        {
            _requestRepository.Serialize();
      
        }

        public void Deserialize()
        {
            _requestRepository.Deserialize();
        }

        public async void ThreadFunction()
        {
            while (true)
            {
                
                try
                {
                    foreach (ExchangeEquipmentRequest request in _requestRepository.GetAllRequest())
                    {
                        if (DateTime.Compare(request.scheduleDate, DateTime.Now) <= 0)
                        {
                            Notify(request);
                            _requestRepository.DeleteRequest(request);
                            Serialize();
                            
                        }
                    }

                }
                catch (Exception e)
                {
                    
                }
                
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
            }

        }

        
    }
}
