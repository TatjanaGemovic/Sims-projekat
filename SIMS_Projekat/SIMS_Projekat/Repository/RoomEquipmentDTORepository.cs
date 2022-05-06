using SIMS_Projekat.DTO;
using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Repository
{
    public class RoomEquipmentDTORepository
    {

        public static List<RoomEquipmentDTO> roomEquipment;
        private Serializer<RoomEquipmentDTO> serializer;
        private string file;
        public static List<Model.Room> _rooms;
        public static List<Model.Equipment> _equipment;

        public List<RoomEquipmentDTO> GetRoomEquipment()
        {
            return roomEquipment;
        }

        public DTO.RoomEquipmentDTO GetRoomEquipmentByID(string dtoID)
        {
            foreach (DTO.RoomEquipmentDTO _roomEquipment in roomEquipment)
            {
                if (_roomEquipment.RoomEquipmentDTOID.Equals(dtoID))
                {
                    return _roomEquipment;
                }
            }

            return null;
        }

        public DTO.RoomEquipmentDTO GetRoomEquipmentByRoomID(string roomID)
        {
            foreach (DTO.RoomEquipmentDTO _roomEquipment in roomEquipment)
            {
                if (_roomEquipment.RoomIDDTO.Equals(roomID))
                {
                    return _roomEquipment;
                }
            }

            return null;
        }

        public DTO.RoomEquipmentDTO GetRoomEquipmentByEquipmentID(string equipmentID)
        {
            foreach (DTO.RoomEquipmentDTO _roomEquipment in roomEquipment)
            {
                if (_roomEquipment.EquipmentIDDTO.Equals(equipmentID))
                {
                    return _roomEquipment;
                }
            }

            return null;
        }

        public RoomEquipmentDTO DeleteRoomEquipment(RoomEquipmentDTO _roomEquipment)
        {
            roomEquipment.Remove(_roomEquipment);
            Serialize();
           // Deserialize(_rooms, _equipment);
            return _roomEquipment;
        }

        public RoomEquipmentDTO EditRoomEquipment(RoomEquipmentDTO oldRoomEquipment, RoomEquipmentDTO newRoomEquipment)
        {
            oldRoomEquipment.QuantityDTO = newRoomEquipment.QuantityDTO;
            Serialize();
            //Deserialize(_rooms, _equipment);
            return newRoomEquipment;
        
        }

        public RoomEquipmentDTO AddRoomEquipment(RoomEquipmentDTO newRoomEquipment)
        {
            roomEquipment.Add(newRoomEquipment);
            Serialize();
            //Deserialize(_rooms, _equipment);
            return newRoomEquipment;
        }


        public List<RoomEquipmentDTO> RoomEquipmentDTO
        {
            get 
            {
                if (roomEquipment == null)
                    roomEquipment = new List<RoomEquipmentDTO>();
                return roomEquipment;
            }
            set 
            {
                RemoveAllRoomEquipment();
                if (value != null)
                {
                    foreach (RoomEquipmentDTO oRoomEquipment in value)
                        AddRoomEquipment(oRoomEquipment);
                }
            }
        }

        public void RemoveAllRoomEquipment()
        {
            if (roomEquipment != null)
                roomEquipment.Clear();
        }

        public RoomEquipmentDTORepository(string fileName)
        {
            roomEquipment = new List<RoomEquipmentDTO>();
            serializer = new Serializer<RoomEquipmentDTO>();
            file = fileName;

        }

        public void Serialize()
        {
            serializer.toCSV(file, roomEquipment);
        }

        public void Deserialize(List<Room> rooms, List<Equipment> equipment)
        {
            _rooms = rooms;
            _equipment = equipment;
            roomEquipment = serializer.fromCSV(file);
            foreach (RoomEquipmentDTO dto in roomEquipment)
            {
                var room = rooms.Find(r => r.RoomID == dto.RoomIDDTO);
                var oEquipment = equipment.Find(e => e.EquipmentID == dto.EquipmentIDDTO);
                room.pEquipment.Add(oEquipment);
                room.pEquipmentQuantity.Add(dto.QuantityDTO);
                oEquipment.Rooms.Add(room);
                oEquipment.ppEquipmentQuantity.Add(dto.QuantityDTO);
            }
        }


    }
}
