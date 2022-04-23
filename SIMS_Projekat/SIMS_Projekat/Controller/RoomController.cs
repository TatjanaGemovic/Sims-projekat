using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMS_Projekat.Controller
{
   public class RoomController
   {
        public RoomController()
        {
            roomService = new RoomService();
        }
        public ObservableCollection<Room> GetRooms()
        {
            return this.roomService.GetRooms();
        }

        public Model.Room GetRoomByID(string roomID)
        {
            return this.roomService.GetRoomByID(roomID);
        }

        public ObservableCollection<Room> GetRoomsByType(Model.RoomType type)
        {
            return this.roomService.GetRoomsByType(type);
        }

        public Model.Room AddRoom(Model.Room newRoom)
        {
            return this.roomService.AddRoom(newRoom);
        }

        public Model.Room DeleteRoomByID(string roomID)
        {
            return this.roomService.DeleteRoomByID(roomID);
        }

        public Model.Room EditRoom(string oldRoomID, Model.Room newRoom)
        {
            return this.roomService.EditRoom(oldRoomID, newRoom);
        }

        public ObservableCollection<Room> GetRoomsByFloor(int floorNumber)
        {
            return this.roomService.GetRoomsByFloor(floorNumber);
        }

        public ObservableCollection<Room> GetAvailableRooms()
        {
            return this.roomService.GetAvailableRooms();
        }

        public void Serialize()
        {
            roomService.Serialize();
        }
        public void Deserialize()
        {
            roomService.Deserialize();
        }
        public RoomService roomService;

    }
}