using SIMS_Projekat.Model;
using SIMS_Projekat.Service;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Controller
{
   public class RoomController
   {
      public List<Room> GetRooms()
      {
         throw new NotImplementedException();
      }
      
      public Model.Room GetRoomByID(string roomID)
      {
         throw new NotImplementedException();
      }
      
      public List<Room> GetRoomsByType(Model.RoomType type)
      {
         throw new NotImplementedException();
      }
      
      public Model.Room AddRoom(Model.Room newRoom)
      {
         throw new NotImplementedException();
      }
      
      public Model.Room DeleteRoomByID(string roomID)
      {
         throw new NotImplementedException();
      }
      
      public Model.Room EditRoom(string oldRoomID, Model.Room newRoom)
      {
         throw new NotImplementedException();
      }
      
      public List<Room> GetRoomsByFloor(int floorNumber)
      {
         throw new NotImplementedException();
      }
      
      public List<Room> GetAvailableRooms()
      {
         throw new NotImplementedException();
      }
      
      public RoomService roomService;
   
   }
}