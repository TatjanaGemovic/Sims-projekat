using SIMS_Projekat.Model;
using System;
using System.Collections.Generic;

namespace SIMS_Projekat.Repository
{
   public class RoomRepository
   {
      public List<Room> GetRooms()
      {
         throw new NotImplementedException();
      }
      
      public Model.Room GetRoomByID(string roomID)
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
      
      public System.Collections.Generic.List<Room> room;
      
      
      
      public System.Collections.Generic.List<Room> Room
      {
         get
         {
            if (room == null)
               room = new System.Collections.Generic.List<Room>();
            return room;
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
      
      
      public void AddRoom(Model.Room newRoom)
      {
         if (newRoom == null)
            return;
         if (this.room == null)
            this.room = new System.Collections.Generic.List<Room>();
         if (!this.room.Contains(newRoom))
            this.room.Add(newRoom);
      }
      
      
      public void RemoveRoom(Model.Room oldRoom)
      {
         if (oldRoom == null)
            return;
         if (this.room != null)
            if (this.room.Contains(oldRoom))
               this.room.Remove(oldRoom);
      }
      
      
      public void RemoveAllRoom()
      {
         if (room != null)
            room.Clear();
      }
   
   }
}