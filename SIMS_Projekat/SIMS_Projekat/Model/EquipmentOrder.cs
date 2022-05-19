using System;

namespace SIMS_Projekat.Model
{
    public class EquipmentOrder : Serialization.Serializable
    {
        public string ID;
        public Equipment Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime ArrivalDate { get; set; }



        public void fromCSV(string[] values)
        {
            ID = values[0];
            Equipment = App.equipmentController.GetEquipmentByID(values[1]);
            Quantity = int.Parse(values[2]);
            ArrivalDate = DateTime.Parse(values[3]);
        }

        public string[] toCSV()
        {
            string[] values =
           {
                ID.ToString(),
                Equipment.EquipmentID.ToString(),
                Quantity.ToString(),
                ArrivalDate.ToString(),
            };
            return values;
        }
    }
}