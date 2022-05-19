using SIMS_Projekat.Model;
using SIMS_Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat.Service
{
    public class EquipmentOrderService
    {
        public EquipmentOrderRepository EquipmentOrderRepository { get; set; }

        public EquipmentOrder AddEquipmentOrder(EquipmentOrder equipmentOrder)
        {
            return EquipmentOrderRepository.AddEquipmentOrder(equipmentOrder);
        }

        public EquipmentOrder DeleteEquipmentOrder(EquipmentOrder equipmentOrder)
        {
            return EquipmentOrderRepository.DeleteEquipmentOrder(equipmentOrder);
        }

        public List<EquipmentOrder> GetAllEquipmentOrders()
        {
            return EquipmentOrderRepository.EquipmentOrders;
        }
        public void Serialize()
        {
            EquipmentOrderRepository.Serialize();
        }

        public void Deserialize()
        {
            EquipmentOrderRepository.Deserialize();
        }

    }
}
