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
        private readonly ExchangeEquipmentRequestRepository _exchangeEquipmentRequestRepository;

        public RenovationRequestService(RenovationRequestRepository renovationRequestRepository, RoomRepository roomRepository, ExchangeEquipmentRequestRepository exchangeEquipmentRequestRepository)
        {
            _requestRepository = renovationRequestRepository;
            _roomRepository = roomRepository;
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

            DisableRoom(request.roomsForRenovation);
            if(!request.roomForMerge.Equals(""))
                DisableRoom(request.roomForMerge);
            request.check = true;
        }

        private void DisableRoom(string roomID)
        {
            var room = _roomRepository.GetRoomByID(roomID);
            room.Available = false;
            _roomRepository.EditRoom(room.RoomID, room);
            if (room.pEquipment.Count > 0)
                ExchangeAllEquipmentFromRoom(room);
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
                var roomForMerge = _roomRepository.GetRoomByID(request.roomForMerge);
                _roomRepository.DeleteRoomByID(request.roomForMerge);
            }
            else if(request.renovationType == RenovationType.Split)
            {
                var newRoom = CreateNewSplitRoom(request.roomsForRenovation);
                while (_roomRepository.AddRoom(newRoom) == null)
                    newRoom.RoomNumber += new Random().Next(1, 100);
            }
        }

        private Room CreateNewSplitRoom(string roomForRenovationID)
        {
            var oldRoom = _roomRepository.GetRoomByID(roomForRenovationID);
            var newRoom = new Room();
            newRoom.RoomID = Guid.NewGuid().ToString();
            newRoom.RoomNumber = oldRoom.RoomNumber + 1;
            newRoom.Floor = oldRoom.Floor;
            newRoom.pRoomType = oldRoom.pRoomType;
            newRoom.Available = true;
            return newRoom;
        }

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
            }

        }
    }
}
