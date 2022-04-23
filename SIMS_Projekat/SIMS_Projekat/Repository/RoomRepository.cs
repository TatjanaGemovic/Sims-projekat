using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Repository
{
   public class RoomRepository
   {
        private ObservableCollection<Room> rooms { get; set; }
        private Serializer<Room> serializer;
        private string file;


        public ObservableCollection<Room> GetRooms()
        {
            return rooms;
        }

        public Model.Room GetRoomByID(string roomID)
        {
            foreach (Room room in this.rooms)
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


            return room;
        }

        public Model.Room EditRoom(string oldRoomID, Model.Room newRoom)
        {
            var oldRoom =GetRoomByID(oldRoomID);
            if (oldRoom != null)
            {
                oldRoom.RoomID = newRoom.RoomID;
                oldRoom.RoomNumber = newRoom.RoomNumber;
                oldRoom.Available = newRoom.Available;
                oldRoom.Floor = newRoom.Floor;
                oldRoom.Type = newRoom.Type;
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
            return NewRoom;
        }



        public ObservableCollection<Room> Room
        {
            get
            {
                if (rooms == null)
                    rooms = new ObservableCollection<Room>();
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
            rooms = new ObservableCollection<Room>();
            serializer = new Serializer<Room>();
            file = fileName;
            Deserialize();
        }

        public void Serialize()
        {
            serializer.toCSV(file, rooms);
        }

        public void Deserialize()
        {
            rooms = serializer.fromCSVObservableCollection(file);
        }

    }
}