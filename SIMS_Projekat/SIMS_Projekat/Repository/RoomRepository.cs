using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Repository
{
   public class RoomRepository
   {
        public static List<Room> rooms { get; set; }
        private Serializer<Room> serializer;
        private string file;


        public List<Room> GetRooms()
        {
            DeserializeInApp();
            return rooms;
        }


        public Model.Room GetRoomByID(string roomID)
        {
            foreach (Room room in rooms)
            {

                if (room.RoomID.Equals(roomID))
                {
                    return room;
                }

            }

            return null;
        }

        public Model.Room DeleteRoomByID(string roomID)
        {
            var room = GetRoomByID(roomID);
            if (room != null)
            {
                rooms.Remove(room);
            }
            else
            {
                return null;
            }

            Serialize();
            return room;
        }

        public Model.Room EditRoom(string oldRoomID, Model.Room newRoom)
        {
            var oldRoom = GetRoomByID(oldRoomID);
            if (oldRoom != null)
            {
                oldRoom.RoomID = newRoom.RoomID;
                oldRoom.RoomNumber = newRoom.RoomNumber;
                oldRoom.Available = newRoom.Available;
                oldRoom.Floor = newRoom.Floor;
                oldRoom.pRoomType = newRoom.pRoomType;
                Serialize();
            }
            else
                return null;

            return newRoom;
        }

        public Model.Room AddRoom(Model.Room NewRoom)
        {
            if (GetRoomByID(NewRoom.RoomID) == null)
                rooms.Add(NewRoom);
            else
                return null;

            Serialize();
            return NewRoom;
        }



        public List<Room> Room
        {
            get
            {
                if (rooms == null)
                    rooms = new List<Room>();
                return rooms;
            }
            set
            {
                RemoveAllRoom();
                if (value != null)
                {
                    foreach (Model.Room oRoom in value)
                        AddRoom(oRoom);
                }
            }
        }

        public void RemoveAllRoom()
        {
            if (rooms != null)
                rooms.Clear();
        }

        public RoomRepository(string fileName)
        {
            rooms = new List<Room>();
            serializer = new Serializer<Room>();
            file = fileName;
        }

        public void Serialize()
        {
            serializer.toCSV(file, rooms);
        }

        public void DeserializeInApp()
        {
            rooms = serializer.fromCSV(file);
            App.roomEquipmentDTORepository.Deserialize(rooms, App.equipmentRepository.GetEquipment());
        }
        public void Deserialize()
        {
            rooms = serializer.fromCSV(file);

        }

    }
}