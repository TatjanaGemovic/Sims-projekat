using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class RoomEquipmentDTOService
    {
        
        private readonly RoomEquipmentDTORepository _roomEquipmentDTORepository;
        public RoomEquipmentDTOService(RoomEquipmentDTORepository roomEquipmentDTORepository)
        {
            _roomEquipmentDTORepository = roomEquipmentDTORepository;
        }
        public void Deserialize(List<Room> rooms, List<Equipment> equipment)
        {
            _roomEquipmentDTORepository.Deserialize(rooms, equipment);
        }
        public List<RoomEquipmentDTO> GetRoomEquipment()
        {
            return this._roomEquipmentDTORepository.GetRoomEquipment();
        }

        public RoomEquipmentDTO GetRoomEquipmentByID(string roomEquipmentID)
        {
            return this._roomEquipmentDTORepository.GetRoomEquipmentByID(roomEquipmentID);
        }

        public RoomEquipmentDTO GetRoomEquipmentByRoomID(string roomID)
        {
            return this._roomEquipmentDTORepository.GetRoomEquipmentByRoomID(roomID);
        }

        public RoomEquipmentDTO GetRoomEquipmentByEquipmentID(string equipmentID)
        {
            return this._roomEquipmentDTORepository.GetRoomEquipmentByEquipmentID(equipmentID);
        }

        public RoomEquipmentDTO AddRoomEquipment(RoomEquipmentDTO newRoomEquipment)
        {
            if(GetRoomEquipmentByID(newRoomEquipment.RoomEquipmentDTOID) == null )
                return this._roomEquipmentDTORepository.AddRoomEquipment(newRoomEquipment);

            return null;
        }

        public RoomEquipmentDTO DeleteRoomEquipment(string equipmentID)
        {
            var roomEquipmentForDelete = GetRoomEquipmentByID(equipmentID);
            if (roomEquipmentForDelete != null)
                return this._roomEquipmentDTORepository.DeleteRoomEquipment(roomEquipmentForDelete);

            return null;
        }

        public RoomEquipmentDTO EditEquipment(string roomEquipmentID, RoomEquipmentDTO newRoomEquipment)
        {
            var roomEquipmentForEdit = GetRoomEquipmentByID(roomEquipmentID);
            if (roomEquipmentForEdit != null)
                return this._roomEquipmentDTORepository.EditRoomEquipment(roomEquipmentForEdit, newRoomEquipment);

            return null;
        }


        public void Serialize()
        {
            _roomEquipmentDTORepository.Serialize();
        }


    }
}
