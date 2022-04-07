using System;

namespace SIMS_Projekat.Model
{
    public class Room : Serialization.Serializable
    {
        public string RoomID { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public Boolean Available { get; set; }

        public void fromCSV(string[] values)
        {
            throw new NotImplementedException();
        }

        public string[] toCSV()
        {
            throw new NotImplementedException();
        }
    }
}