using SIMS_Projekat.Model;
using SIMS_Projekat.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Projekat.Repository
{
    public class EquipmentOrderRepository
    {
        public List<EquipmentOrder> EquipmentOrders { get; set; }
        private Serializer<EquipmentOrder> equipmentOrderSerializer;

        private string equipmentOrderFile;

        private int ID;


        public EquipmentOrderRepository(string equipmentOrderFileName)
        {
            EquipmentOrders = new List<EquipmentOrder>();
            equipmentOrderSerializer = new Serializer<EquipmentOrder>();
            equipmentOrderFile = equipmentOrderFileName;
            ID = 100;
        }

        public void Serialize()
        {
            equipmentOrderSerializer.toCSV(equipmentOrderFile, EquipmentOrders);
        }

        public void Deserialize()
        {
            EquipmentOrders = equipmentOrderSerializer.fromCSV(equipmentOrderFile);

            int maxID = 100;
            foreach (EquipmentOrder equipmentOrder in EquipmentOrders)
            {
                if (int.Parse(equipmentOrder.ID) > maxID)
                    maxID = int.Parse(equipmentOrder.ID);
            }
            ID = ++maxID;
        }

        public EquipmentOrder AddEquipmentOrder(EquipmentOrder equipmentOrder)
        {
            equipmentOrder.ID = ID++.ToString();
            EquipmentOrders.Add(equipmentOrder);
            return equipmentOrder;
        }

        public EquipmentOrder DeleteEquipmentOrder(EquipmentOrder equipmentOrder)
        {
            return EquipmentOrders.Remove(equipmentOrder) ? equipmentOrder : null;
        }

        public EquipmentOrder GetEquipmentOrderByID(string ID)
        {
            foreach(EquipmentOrder equipmentOrder in EquipmentOrders)
            {
                if (equipmentOrder.ID.Equals(ID)) { return equipmentOrder; }
            }
            return null;
        }

    }
}
