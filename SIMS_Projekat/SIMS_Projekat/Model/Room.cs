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
            RoomID = values[0];
            RoomNumber = int.Parse(values[1]);
            Floor = int.Parse(values[2]);
            Type = (RoomType)int.Parse(values[3]);
            if (values[4].Equals("true")) { Available = true; } else { Available = false; }
        }

        public string[] toCSV()
        {
            string[] values = {
                RoomID,
                RoomNumber.ToString(),
                Floor.ToString(),
                ((int)Type).ToString(),
                Available.ToString()
            };

            return values;
        }
    }
}