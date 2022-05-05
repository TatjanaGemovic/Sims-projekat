using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Controller
{
   public class RoomController
   {
        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public List<Room> GetRooms()
        {
            return this._roomService.GetRooms();
        }

        public Model.Room GetRoomByID(string roomID)
        {
            return this._roomService.GetRoomByID(roomID);
        }

        public List<Room> GetRoomsByType(Model.RoomType type)
        {
            return this._roomService.GetRoomsByType(type);
        }

        public Model.Room AddRoom(Model.Room newRoom)
        {
            return this._roomService.AddRoom(newRoom);
        }

        public Model.Room DeleteRoomByID(string roomID)
        {
            return this._roomService.DeleteRoomByID(roomID);
        }

        public Model.Room EditRoom(string oldRoomID, Model.Room newRoom)
        {
            return this._roomService.EditRoom(oldRoomID, newRoom);
        }

        public List<Room> GetRoomsByFloor(int floorNumber)
        {
            return this._roomService.GetRoomsByFloor(floorNumber);
        }

        public List<Room> GetAvailableRooms()
        {
            return this._roomService.GetAvailableRooms();
        }

        public List<Room> GetAvailableNotMeetingRooms()
        {
            return this._roomService.GetAvailableNotMeetingRooms();
        }

        public List<Room> GetAvailableNotMeetingRoomsExcept(string exceptRoomID)
        {
            return this._roomService.GetAvailableNotMeetingRoomsExcept(exceptRoomID);
        }

        public void Serialize()
        {
            _roomService.Serialize();
        }
        public void Deserialize()
        {
            _roomService.Deserialize();
        }
        private readonly RoomService _roomService;

    }
}