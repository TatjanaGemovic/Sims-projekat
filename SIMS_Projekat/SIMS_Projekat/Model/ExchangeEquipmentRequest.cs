using SIMS_Projekat.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Model
{
    public class ExchangeEquipmentRequest : Serialization.Serializable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string requestID{get; set;}
        public string toRoomID {get;set;}
        public string fromRoomID { get; set; }
        public string equipmentID { get; set; }
        public int quantity { get; set; }

        public DateTime scheduleDate { get; set; }

        public Boolean allEquipmentFromRoom { get; set; }

        public void fromCSV(string[] values)
        {
            toRoomID = values[0];
            fromRoomID = values[1];
            equipmentID = values[2];
            quantity = int.Parse(values[3]);
            scheduleDate = DateTime.Parse(values[4]);
            requestID = values[5];
            if (values[6].Equals("True")) { allEquipmentFromRoom = true; } else { allEquipmentFromRoom= false; }
        }

        public string[] toCSV()
        {
            string[] values = {
                toRoomID,
                fromRoomID,
                equipmentID,
                quantity.ToString(),
                scheduleDate.ToString(),
                requestID,
                allEquipmentFromRoom.ToString()
            };

            return values;
        }

        

    }
}
