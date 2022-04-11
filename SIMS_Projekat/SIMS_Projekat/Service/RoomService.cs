using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Service
{
   public class RoomService
   {
        private static string ROOM_CSV = @".\..\..\..\Resources\rooms.txt";
        public RoomService()
        {
            roomRepository = new RoomRepository(ROOM_CSV);
        }

        public ObservableCollection<Room> GetRooms()
        {
            return this.roomRepository.GetRooms();
        }

        public Model.Room GetRoomByID(string roomID)
        {
            return this.roomRepository.GetRoomByID(roomID);
        }

        public ObservableCollection<Room> GetRoomsByType(Model.RoomType type)
        {
            var allRooms = this.roomRepository.GetRooms();
            var filterRooms = new ObservableCollection<Room>();
            foreach (Room r in allRooms)
            {
                if (r.Type == type)
                {
                    filterRooms.Add(r);
                }
            }

            return filterRooms;

        }

        public Model.Room AddRoom(Model.Room newRoom)
        {
            return this.roomRepository.AddRoom(newRoom);
        }

        public Model.Room DeleteRoomByID(string roomID)
        {
            return this.roomRepository.DeleteRoomByID(roomID);
        }

        public Model.Room EditRoom(string oldRoomID, Model.Room newRoom)
        {
            return this.roomRepository.EditRoom(oldRoomID, newRoom);
        }

        public ObservableCollection<Room> GetRoomsByFloor(int floorNumber)
        {
            var allRooms = this.roomRepository.GetRooms();
            var filterRooms = new ObservableCollection<Room>();
            foreach (Room room in allRooms)
            {
                if (room.Floor == floorNumber)
                {
                    filterRooms.Add(room);
                }
            }

            return filterRooms;
        }

        public ObservableCollection<Room> GetAvailableRooms()
        {
            var allRooms = this.roomRepository.GetRooms();
            var filterRooms = new ObservableCollection<Room>();
            foreach (Room room in allRooms)
            {
                if (room.Available == true)
                {
                    filterRooms.Add(room);
                }
            }

            return filterRooms;
        }

        public void Serialize()
        {
            roomRepository.Serialize();
        }

        public RoomRepository roomRepository;

    }
}