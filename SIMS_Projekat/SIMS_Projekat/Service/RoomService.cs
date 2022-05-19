using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Service
{
   public class RoomService
   {
        private readonly RoomRepository _roomRepository;
        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public void Deserialize()
        {
            _roomRepository.Deserialize();
        }
        public List<Room> GetRooms()
        {
            return this._roomRepository.GetRooms();
        }

        public Model.Room GetRoomByID(string roomID)
        {
            return this._roomRepository.GetRoomByID(roomID);
        }

        public List<Room> GetRoomsByType(Model.RoomType type)
        {
            var allRooms = this._roomRepository.GetRooms();
            var filterRooms = new List<Room>();
            foreach (Room r in allRooms)
            {
                if (r.pRoomType == type)
                {
                    filterRooms.Add(r);
                }
            }

            return filterRooms;

        }

        public Model.Room AddRoom(Model.Room newRoom)
        {
            return this._roomRepository.AddRoom(newRoom);
        }

        public Model.Room DeleteRoomByID(string roomID)
        {
            return this._roomRepository.DeleteRoomByID(roomID);

        }

        public Model.Room EditRoom(string oldRoomID, Model.Room newRoom)
        {
            return this._roomRepository.EditRoom(oldRoomID, newRoom);
        }

        public List<Room> GetRoomsByFloor(int floorNumber)
        {
            var allRooms = this._roomRepository.GetRooms();
            var filterRooms = new List<Room>();
            foreach (Room room in allRooms)
            {
                if (room.Floor == floorNumber)
                {
                    filterRooms.Add(room);
                }
            }

            return filterRooms;
        }

        public List<Room> GetAvailableRooms()
        {
            var allRooms = this._roomRepository.GetRooms();
            var filterRooms = new List<Room>();
            foreach (Room room in allRooms)
            {
                if (room.Available == true)
                {
                    filterRooms.Add(room);
                }
            }

            return filterRooms;
        }

        public List<Room> GetAvailableRooms(DateTime time)
        {
            List<Room> rooms = _roomRepository.GetRooms();
            List<Appointment> appointments = App.appointmentService.GetAllAppointments();
            bool a;
            foreach (Appointment appointment in appointments)
            {
                if (appointment.beginningDate == time)
                    rooms.RemoveAll(x => x.RoomID == appointment.room.RoomID);

            }

            return rooms;
        }


        public List<Room> GetAvailableNotMeetingRooms()
        {
            var allRooms = this._roomRepository.GetRooms();
            var filterRooms = new List<Room>();
            foreach (Room room in allRooms)
            {
                if (room.Available == true && room.pRoomType != RoomType.meetingRoom)
                {
                    filterRooms.Add(room);
                }
            }

            return filterRooms;
        }

        public List<Room> GetAvailableNotMeetingRoomsExcept(string exceptRoomID)
        {
            var allRooms = this._roomRepository.GetRooms();
            var filterRooms = new List<Room>();
            foreach (Room room in allRooms)
            {
                if (room.Available == true && room.pRoomType != RoomType.meetingRoom && exceptRoomID!=room.RoomID)
                {
                    filterRooms.Add(room);
                }
            }

            return filterRooms;
        }

        public void Serialize()
        {
            _roomRepository.Serialize();
        }

    }
}