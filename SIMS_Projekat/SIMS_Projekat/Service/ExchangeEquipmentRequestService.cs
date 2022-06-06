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
        
        private readonly UnitService _unitService;
        

        public ExchangeEquipmentRequestService()
        {
            _unitService = new UnitService();

        }

        public List<ExchangeEquipmentRequest> GetAllRequest()
        {
            return _unitService._exchangeEquipmentRequestRepository.GetAll();
        }

        public ExchangeEquipmentRequest GetRequestByID(string id)
        {
            return _unitService._exchangeEquipmentRequestRepository.GetByID(id);
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
                var dtoToChange = _unitService._roomEquipmentDTORepository.GetRoomEquipmentByID(request.toRoomID+request.equipmentID);
                if (dtoToChange != null)
                {
                    updateDTO(request, 1);
                }
                else
                {
                    var newDTO = createDTO(request);
                    _unitService._roomEquipmentDTORepository.AddRoomEquipment(newDTO);

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
                {
                    deleteDTOData(request);
                }
                else
                {
                    var dtoToChange = _unitService._roomEquipmentDTORepository.GetRoomEquipmentByID(request.fromRoomID + request.equipmentID);
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
            var oldEquipment = _unitService._equipmentRepository.GetEquipmentByID(request.equipmentID);
            var newEquipment = _unitService._equipmentRepository.GetEquipmentByID(request.equipmentID);
            if (oldEquipment != null)
            {
                if(mood==1)
                    newEquipment.Quantity += request.quantity;
                else
                    newEquipment.Quantity -= request.quantity;
                
            }
            _unitService._equipmentRepository.EditEquipment(oldEquipment, newEquipment);
        }


        private void updateDTO( ExchangeEquipmentRequest request, int mood)
        {
            var dtoForChange = _unitService._roomEquipmentDTORepository.GetRoomEquipmentByID(request.toRoomID + request.equipmentID);
            var newDTO = _unitService._roomEquipmentDTORepository.GetRoomEquipmentByID(request.toRoomID + request.equipmentID);
            if (mood == 1)
            {
                newDTO.QuantityDTO += request.quantity;
            }
            else 
            {
                newDTO.QuantityDTO -= request.quantity;
            }
            _unitService._roomEquipmentDTORepository.EditRoomEquipment(dtoForChange, newDTO);
        }

        private void deleteDTOData(ExchangeEquipmentRequest request)
        {
            var dtoFromRepository = _unitService._roomEquipmentDTORepository.GetRoomEquipmentByID(request.fromRoomID+ request.equipmentID);
            if(dtoFromRepository!= null)
                _unitService._roomEquipmentDTORepository.DeleteRoomEquipment(dtoFromRepository);

        }
        public ExchangeEquipmentRequest AddRequest(ExchangeEquipmentRequest newRequest)
        {
            return _unitService._exchangeEquipmentRequestRepository.Add(newRequest);
        }
        public ExchangeEquipmentRequest DeleteRequest(ExchangeEquipmentRequest deleteRequest)
        {
            if (GetRequestByID(deleteRequest.requestID) != null)
            {
                return _unitService._exchangeEquipmentRequestRepository.Delete(deleteRequest);
            }

            return null;

        }


        public void Serialize()
        {
            _unitService._exchangeEquipmentRequestRepository.Serialize();
      
        }

        public void Deserialize()
        {
            _unitService._exchangeEquipmentRequestRepository.Deserialize();
        }

        public async void ThreadFunction()
        {
            while (true)
            {
                
                try
                {
                    foreach (ExchangeEquipmentRequest request in _unitService._exchangeEquipmentRequestRepository.GetAll())
                    {
                        if (DateTime.Compare(request.scheduleDate, DateTime.Now) <= 0)
                        {
                            Notify(request);
                            _unitService._exchangeEquipmentRequestRepository.Delete(request);
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
