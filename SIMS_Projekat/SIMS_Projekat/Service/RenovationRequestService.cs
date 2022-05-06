using SIMS_Projekat.DTOModel;
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
    public class RenovationRequestService
    {
        private readonly RenovationRequestRepository _requestRepository;
        private readonly RoomRepository _roomRepository;
        //private readonly RenovationRoomDTORepository _renovationRoomRepository;
        private readonly ExchangeEquipmentRequestRepository _exchangeEquipmentRequestRepository;

        public RenovationRequestService(RenovationRequestRepository renovationRequestRepository, RoomRepository roomRepository, ExchangeEquipmentRequestRepository exchangeEquipmentRequestRepository)
        {
            _requestRepository = renovationRequestRepository;
            _roomRepository = roomRepository;
            //_renovationRoomRepository = renovationRoomDTORepository;
            _exchangeEquipmentRequestRepository = exchangeEquipmentRequestRepository;

        }

        public List<RenovationRequest> GetAllRequest()
        {
            return _requestRepository.GetAllRequest();
        }

        public RenovationRequest GetRequestByID(string id)
        {
            return _requestRepository.GetRequestByID(id);
        }
        public void NotifyStart(RenovationRequest request)
        {
            
                var newRoom = _roomRepository.GetRoomByID(request.roomsForRenovation);
                newRoom.Available = false;
                _roomRepository.EditRoom(newRoom.RoomID, newRoom);
                if(newRoom.pEquipment.Count>0)
                    ExchangeAllEquipmentFromRoom(newRoom);
               
            

            request.check = true;
        }

        
        public void NotifyEnd(RenovationRequest request)
        {

            var newRoom = _roomRepository.GetRoomByID(request.roomsForRenovation);
            newRoom.Available = true;
            _roomRepository.EditRoom(newRoom.RoomID, newRoom);
           
            RunRenovationType(request);
        }

        private void ExchangeAllEquipmentFromRoom(Room room)
        {
            int br = 0;
            foreach (Equipment equipmentFromRoom in room.pEquipment)
            {
                var newExchangeRequest = new ExchangeEquipmentRequest();
                newExchangeRequest.requestID = "magacin" + equipmentFromRoom.EquipmentID;
                newExchangeRequest.fromRoomID = room.RoomID;
                newExchangeRequest.toRoomID = "magacin";
                newExchangeRequest.equipmentID = equipmentFromRoom.EquipmentID;
                newExchangeRequest.quantity = equipmentFromRoom.ppEquipmentQuantity[br];
                newExchangeRequest.allEquipmentFromRoom = true;
                newExchangeRequest.scheduleDate = DateTime.Now;
                _exchangeEquipmentRequestRepository.AddRequest(newExchangeRequest);
                br++;
            }
        }

        private void RunRenovationType(RenovationRequest request)
        {
            if (request.renovationType == RenovationType.Merge)
            {
                //obrisi ostale prostorije ostavi samo jednu
            }
            else
            { 
                //napravi jos jednu prostoriju, eventualno prvoj promeni br u 101a
            }
        }

        /*private void DeleteAllDTO(string requestID)
        {
            var listOfDTO = _renovationRoomRepository.GetRoomRenovationByRequestID(requestID);
            foreach (RenovationRoomDTO dto in listOfDTO)
            {
                _renovationRoomRepository.DeleteRoomRenovation(dto);
            }
        }*/
        public RenovationRequest AddRequest(RenovationRequest newRequest)
        {
            return _requestRepository.AddRequest(newRequest);
        }
        public RenovationRequest DeleteRequest(RenovationRequest deleteRequest)
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
                    foreach (RenovationRequest request in App.renovationRequestController.GetAllRequests())
                    {
                        if (request.check != true)
                        {
                            if (DateTime.Compare(request.scheduleDateStart, DateTime.Now) <= 0)
                            {
                                
                                    NotifyStart(request);
                                    Serialize();
                                
                            }
                        }
                        else
                        {
                            if (DateTime.Compare(request.scheduleDateEnd, DateTime.Now) <= 0)
                            {

                                NotifyEnd(request);
                                //DeleteAllDTO(request.requestID);
                                DeleteRequest(request);
                                Serialize();
                                
                            }
                        }

                    }

                }
                catch (Exception e)
                {

                }

                await Task.Delay(TimeSpan.FromMilliseconds(10));
               // Thread.Sleep(60 * 1000);
            }

        }
    }
}
